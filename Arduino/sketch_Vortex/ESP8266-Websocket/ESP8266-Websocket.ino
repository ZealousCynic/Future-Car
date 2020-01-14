/*
  Name:    Vortex_Websocket.ino
  Created: 1/8/2020 10:54:08 AM
  Author:  zbcannie43
*/

#include <Arduino.h>

#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <WebSocketsServer.h>
#include <Hash.h>

ESP8266WiFiMulti WiFiMulti;
IPAddress ip;

WebSocketsServer webSocket = WebSocketsServer(81);

#ifndef STASSID
#define STASSID "Vortex"
#define STAPSK  "skodLinksys"
#endif

const char* ssid = STASSID;
const char* ssid_password = STAPSK;
byte * receivedBuffer;

void webSocketEvent(uint8_t num, WStype_t type, uint8_t* payload, size_t length)
{
  switch (type)
  {
    case WStype_DISCONNECTED:
      break;
    case WStype_CONNECTED:
      {
        IPAddress ip = webSocket.remoteIP(num);
      }
      break;
    case WStype_TEXT:
      transmitToAtmega(payload, length);
      break;
  }
}

void setup()
{
  WiFiMulti.addAP(ssid, ssid_password);
  while (WiFiMulti.run() != WL_CONNECTED)
  {
    delay(100);
  }
  webSocket.begin();
  webSocket.onEvent(webSocketEvent);

  //Default value for the Robotdyn esp/atmega chip communication
  Serial.begin(115200);
}

void loop() {
  webSocket.loop();

//int, int, int, bool, enum =
// 2 + 2 + 2 + 1 + 2
  if (Serial.available() >= 5) {
    serialReceived();
  }
}

void transmitToAtmega(uint8_t* payload, size_t length) {
  Serial.write(payload, length);
}

void serialReceived() {
  
  size_t len = Serial.available();
  int acc = 0;

  receivedBuffer = (byte *)malloc(len);

  //Incorrect cast -- the first 6 values represent 3 ints -- 
  //this bungles whatever we get, but we can read the last 2 bytes (booleans) correctly
  while (acc < len) {
    *(receivedBuffer + acc) = Serial.read();
    acc++;
  }
  acc = 0;

  //I really need to look into actual serialization/deserialization of a struct.
  int temperature = (int)*(receivedBuffer + acc);  
  acc += 2;
  int compas = (int)*(receivedBuffer + acc);
  acc += 2;
  int waterlevel = (int)*(receivedBuffer + acc);
  acc += 2;
  bool power = (bool)*(receivedBuffer + acc);
  acc++;
  bool state = (bool)*(receivedBuffer + acc);

  String message;
  message += "T ";
  message += temperature;
  message += " C: ";
  message += compas;
  message += " W: ";
  message += waterlevel;
  message += " P: ";
  message += power;
  message += " S: ";
  message += state;
  webSocket.broadcastTXT(message);
  
  free(receivedBuffer);
}
