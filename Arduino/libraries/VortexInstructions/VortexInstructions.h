#include <stdint.h>

typedef enum _V_Instruction {
	DRIVE = 0,
} VortexInstruction;

typedef enum _V_State {
	LAND = 0,
	WATER = 1
} VortexState;

typedef enum _Stepper_Operation {
	S_OFF = 0,
	S_FORWARD = 1,
	S_BACKWARD = 2
} StepperOperation;

typedef enum _Servo_Operation {
	S_NEUTRAL = 90,
	S_LEFT = 45,
	S_RIGHT = 135
} ServoOperation;

typedef enum _Pump_Operation {
	P_OFF = 0,
	P_ON = 1
} PumpOperation;

typedef enum _Valve_Operation {
	V_NEUTRAL = 0,
	V_LEFT = 1,
	V_RIGHT = 2
} ValveOperation;

typedef struct _readings {
	int16_t temperature;
	uint16_t compas;
	uint16_t waterlevel;
	bool power;
	bool state;
} ReadingSet;

ReadingSet * getReading() {
	return (ReadingSet*)malloc(sizeof(ReadingSet));
}

void initReading(ReadingSet * toInit,int16_t _temp, uint16_t _compas, uint16_t _waterlevel, bool _power, bool _state) {
	toInit->temperature = _temp;
	toInit->compas = _compas;
	toInit->waterlevel = _waterlevel;
	toInit->power = _power;
	toInit->state = _state;
}