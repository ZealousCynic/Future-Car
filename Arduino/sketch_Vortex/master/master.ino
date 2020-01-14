#include <Wire.h>
#include <stdint.h>
#include "VortexInstructions.h"

//Bad form, having points staticly allocated.
byte* toParsePtr;
byte* ESPtoParsePtr;
size_t len;
ReadingSet* toSend;

bool readingFlag;
bool espReceived;

void setup() {

  //Default for communication between atmega and onboard esp.
  //This board is nonstandard: The ESP chip is onboard, and joined directly to the hardware serial bus
  //No need to create a software instance to listen to.
  Serial.begin(115200);

  Wire.begin(1); //Initialize and join the I2C bus as device 1.
  Wire.onReceive(I2CReceive); //Register I2CReceive as interrupt handler for onReceive.

  //On startup, nothing is true. Everything is permitted.
  readingFlag = false;
  espReceived = false;
}

void loop() {

delay(500);
  //Serial.println("Looping");

  if (0 < Serial.available() && !espReceived)
  {
    ESPReceive();
  }

  if (espReceived)
  {
    espReceived = false;

    Wire.beginTransmission(5);
    Wire.write(ESPtoParsePtr, len);
    Wire.endTransmission();

    free(ESPtoParsePtr);
  }
  //Write test bytes to Hardware controller device if we haven't actually received anything
  else {
  }

  //Have we received bytes?
  if (readingFlag)
  {
    GetI2CReading();
    sendToSerial();
  }
    
}

byte * getArray(int size) {
  //Return byte[size] array
  return (byte*)malloc(size * sizeof(byte));
}

//Assume the received array is a series of numbers
//Assume those numbers are represented by their ASCII values.
//Convert their ASCII values to their corresponding byte values

//We need something like: uint8_t * payload, size_t length
void ESPReceive() {

  int acc = 0;
  len = Serial.available();
  ESPtoParsePtr = getArray(len);

  while (acc < len) {
    *(ESPtoParsePtr + acc) = Serial.read();
    acc++;
  }

  espReceived = true;
}

void I2CReceive(int howMany) {

  int acc = 0;

  //Get a pointer to a byte array
  toParsePtr = getArray(Wire.available());

  //
  while (0 < Wire.available()) {
    *(toParsePtr + acc) = Wire.read();
    acc++;
  }

  readingFlag = true;
}

void GetI2CReading() {

  //Data is structured as sequential bytes
  //An int, in AVR/Arduino, is 2 bytes
  //High byte: ((*(toParsePtr + acc + 1) << 8)
  //toParsePtr: Pointer to data
  //acc: Offset into array of data -- which index should be read?
  //+1: toParsePtr + acc points to the beginning of data -- +1 gets us to the high end.

  readingFlag = false;
  int acc = 0;

  int temperature = ((*(toParsePtr + acc + 1) << 8) + * (toParsePtr + acc));
  acc += 2;
  int compas = ((*(toParsePtr + acc + 1) << 8) + * (toParsePtr + acc));
  acc += 2;
  int waterlevel = ((*(toParsePtr + acc + 1) << 8) + * (toParsePtr + acc));
  acc += 2;
  bool power = *(toParsePtr + acc);
  acc++;
  bool state = *(toParsePtr + acc);

  free(toParsePtr);

  toSend = getReading();
  initReading(toSend, temperature, compas, waterlevel, power, state);

  //This should be done only when reading is no longer needed
  
}

void sendToSerial() {
  Serial.write(toSend->temperature);
  Serial.write(toSend->compas);
  Serial.write(toSend->waterlevel);
  Serial.write(toSend->power);
  Serial.write(toSend->state);

  free(toSend);
}
