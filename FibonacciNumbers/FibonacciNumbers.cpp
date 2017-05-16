#include <iostream>
#include <string>

void showFibonacciNumbers(int num);
int getFibonacciNumber(int num);

int main()
{
	const int length = 30;

	showFibonacciNumbers(length);
}

void showFibonacciNumbers(int length)
{
	for (int i = 0; i < length; i++)
	{
		std::cout << getFibonacciNumber(i) << " ";
		//適度に改行
		if (i != 0 && i % 10 == 0) std::cout << '\n';
	} 
	std::cout << '\n';
}

int getFibonacciNumber(int num)
{
	//一つ前の数
	int oneBefore = 0;
	//2つ前の数
	int twoBefore = 0;

	int result = 0;

	if (num >= 1)
	{
		result = 1;
		oneBefore = 1;
	}

	for (int i = 1; i <= num; i++)
	{
		//直前の2項の和
		result = oneBefore + twoBefore;

		twoBefore = oneBefore;
		oneBefore = result;
	}

	return result;
}