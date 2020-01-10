#include <AFMotor.h>
#include <Wire.h>
#include <Servo.h>
#include <string.h>
#include "VortexInstructions.h"

AF_Stepper motor(200, 1);
Servo wServo;

ServoOperation servoOp = S_NEUTRAL;
StepperOperation stepperOp = S_OFF;
PumpOperation pumpOp = P_OFF;
ValveOperation valveOp = V_NEUTRAL;
VortexState state = LAND;

const int TPIN = 0; //Temperature Pin
const int WPIN = 1; //WaterLevel Pin
const int PPIN = 2; //Pump pin

bool power = 1;

//Bad practice -- needed to get it out of the interrupt handler though.
byte* recPtr;

bool readingFlag; //Is sensor readings requested?
bool receivedFlag; //Have bytes been received on I2C?

int acc = 0; //Accumulator

void setup() {
  Serial.begin(9600);
  //Wire library (I2C) initialization
  Wire.begin(5); // Start I2C slave
  Wire.onReceive(I2CReceive); // Register I2CReceive as the interrupthandler for onReceive

  //Stepper motor initialization
  motor.setSpeed(120);  // 120 rpm

  //Servo motor initialization
  wServo.attach(9);

  //Configure digital pin 2 as output -- This pin is used to switch the independent 12V circuit
  pinMode(PPIN, OUTPUT);

  //On startup, nothing is true. Everything is permitted.
  readingFlag = false;
  receivedFlag = false;
}

void loop() {
  //If the Vortex isn't powered on, skip all movement.
  if (power) {

    //State == true == Land based movement
    if (state) {
      stepperOperation(stepperOp);
      servoOperation(servoOp);
    }
    else {
      pumpOperation(pumpOp);
      valveOperation(valveOp);
    }

    //If readings have been requested, gather and send a set
    if (readingFlag)
    {
      readingFlag = false; //Reset flag
      sendReadingSet();
    }
    if (receivedFlag)
    {
      receivedFlag = false; //Reset flag
      evaluateReceived();
    }
  }
  else {
    awaitPower();
  }
}

//I2CListen picks up instructions.
//Instructions are received as bytes
//Depending on the instruction, differently sized arrays can be assumed.
void I2CReceive(int howMany) {

  acc = 0;

  //Get a pointer to an array
  recPtr = getArray(Wire.available());

  //Copy data from I2C bus to array
  while (0 < Wire.available()) {
    *(recPtr + acc) = Wire.read();
    *(recPtr + acc) -= 48;
    acc++;
  }
  receivedFlag = true;
}

void evaluateReceived() {
  Serial.print("RecPtr 1: ");
  Serial.println(*(recPtr));
  switch (*recPtr) {
    case 1:
      power = !power;
      break;
    case 2:
      drive();
      break;
    case 3:
      turn();
      break;
    case 4:
      if (state = LAND)
        state = WATER;
      else
        state = LAND;
      break;
    case 5:
      sendReadingSet();
    default:
      break;
  }

  free(recPtr);
}

void awaitPower() {
  switch (*recPtr) {
    case 1:
      power = !power;
      break;
    default:
      break;
  }
}

//Calculation varies by sensor
int getTemperature() {
  float voltage = analogRead(TPIN);
  voltage /= 1024;
  float t = (voltage - 0.5) * 100;
  return (int)t;
}

int getCompas() {
  //Compas sensor has gone missing -- placeholder
  return 0;
}

byte * getArray(int size) {
  //Return a byte pointer to an array with the indicated number of spaces
  return (byte*)malloc(size * sizeof(byte));
}

ReadingSet * getReadingSet() {
  //Get a pointer to a reading struct
  ReadingSet * toReturn = getReading();

  int temperature = getTemperature();
  int waterlevel = analogRead(WPIN);
  int compas = getCompas();

  //Initialize the struct pointed to
  initReading(toReturn, temperature, compas, waterlevel, power, state);

  //Return the pointer
  return toReturn;
}

void sendReadingSet() {

  ReadingSet* toSend = getReadingSet();

  Wire.beginTransmission(1);
  Wire.write((byte*)toSend, sizeof(ReadingSet));
  Wire.endTransmission();

  free(toSend);
}

void drive() {
  switch (state) {
    case LAND:
      stepperOp = (*(recPtr + 1));
      break;
    case WATER:
      pumpOp = (*(recPtr + 1));
  }
}

void turn() {
  switch (state) {
    case LAND:
      turnServo();
      break;
    case WATER:
      turnValve();
  }
}

void turnServo() {
  switch (*(recPtr + 1)) {
    case 0:
      servoOp = S_NEUTRAL;
      break;
    case 1:
      servoOp = S_RIGHT;
      break;
    case 2:
      servoOp = S_LEFT;
      break;
    default:
      break;
  }
}

void turnValve() {
  //No time to implement -- no circuits.
}

void stepperOperation(StepperOperation op) {

  switch (op) {
    case S_OFF:
      //Motor off, do nothing.
      break;
    case S_FORWARD:
      motor.step(200, FORWARD, SINGLE);
      break;
    case S_BACKWARD:
      motor.step(200, BACKWARD, SINGLE);
    default:
      break;
  }
}

void servoOperation(ServoOperation op) {
  wServo.write(op);
}

void pumpOperation(PumpOperation op) {
  switch (op) {
    case P_OFF:
      digitalWrite(PPIN, LOW);
      break;
    case P_ON:
      digitalWrite(PPIN, HIGH);
      break;
    default:
      break;
  }
}

void valveOperation(ValveOperation op) {

}
