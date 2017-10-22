#include <cstdint>

extern "C"
{
	__declspec(dllexport) uint32_t state;

	__declspec(dllexport) uint32_t getState()
	{
		return state;
	}

	__declspec(dllexport) void setState(uint32_t _state)
	{
		state = _state;
	}
}
