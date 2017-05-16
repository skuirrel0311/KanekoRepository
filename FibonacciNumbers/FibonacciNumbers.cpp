#include <iostream>
#include <string>

void showFibonacciNumbers(int num);
int getFibonacciNumber(int num);

int main()
{
	const int num = 16;

	showFibonacciNumbers(num);
}

void showFibonacciNumbers(int num)
{
	for (int i = 0; i <= num; i++)
	{
		getFibonacciNumber(i);
		std::cout << getFibonacciNumber(i) << " ";
	}
}

int getFibonacciNumber(int num)
{
	//ˆê‚Â‘O‚Ì”
	int buf1 = 0;
	//2‚Â‘O‚Ì”
	int buf2 = 0;

	int result = 0;

	for (int i = 0; i <= num; i++)
	{
		if (i <= 1) result = 1;
		else result = buf1 + buf2;

		buf2 = buf1;
		buf1 = num;
	}

	return result;
}