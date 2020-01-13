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

  if (Serial.available() > 0) {
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
    *(receivedBuffer + acc) += 48;
    acc++;
  }

  //String message = String((char * ) &receivedBuffer[0]);
  webSocket.broadcastTXT(receivedBuffer, len);

  free(receivedBuffer);
}
