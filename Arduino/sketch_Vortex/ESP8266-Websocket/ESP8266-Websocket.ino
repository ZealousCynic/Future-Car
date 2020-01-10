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

uint8_t* testVals;
uint8_t toSend[2] = {51, 48};
uint8_t change[2] = {50, 50};

const char* ssid = STASSID;
const char* ssid_password = STAPSK;

void webSocketEvent(uint8_t num, WStype_t type, uint8_t* payload, size_t length)
{
    switch (type)
    {
      case WStype_DISCONNECTED:
          break;
      case WStype_CONNECTED:
          {IPAddress ip = webSocket.remoteIP(num);}
          break;
      case WStype_TEXT:
          transmitToAtmega(payload, length);
          break;
    }
}

void setup()
{
    initTestVals();
  
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
    // send data to all connected clients
    // webSocket.broadcastTXT("message here");
    
//    transmitToAtmega(testVals, 2);
//    delay(2000);
//    transmitToAtmega(toSend, 2);
//    delay(2000);
//    transmitToAtmega(change, 2);
//    delay(2000);
}

void transmitToAtmega(uint8_t* payload, size_t length) {
  Serial.write(payload, length);
}

void initTestVals() {
  testVals = (uint8_t*)malloc(sizeof(uint8_t) * 2);

  *testVals = 50;
  *(testVals + 1) = 49;
  }
