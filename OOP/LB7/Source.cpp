#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <io.h>
#include <string.h>
#include <conio.h>
#include <locale.h>
#include <ctype.h>
#include <stdlib.h>
#include <windows.h>

void fwr(char* str);
void frd(char* str);
void enc(char* str, char* key);
void dec(char* str, char* key);

int main()
{
	setlocale(LC_CTYPE, "Russian");

	int num;
	char str[100];
	char key[100];

	do
	{
		printf("Выберите действие: \n 1 - Сформировать с клавиатуры строку  и записать в файл \n 2 - Считать строку из файла и распечатать \n 3 - Зашифровать строку и записать в файл \n 4 - Расшифровать строку и записать в файл \n 5 - Выход \n");
		scanf("%d", &num);

		switch (num)
		{
		case 1:fwr(str);
			break;
		case 2:frd(str);
			break;
		case 3:enc(str, key);
			break;
		case 4:dec(str, key);
			break;
		case 5:return 0;
		default:
			continue;
		}
	}
	while (1);
	return 0;
	_getch();
}
void fwr(char* str)
{
	FILE *f;
	if ((f = fopen("D:\\text.txt", "w+t"))==NULL)
		printf("Файл невозможно создать \n");
	else
	{
		printf("Введите строку : \n");
		scanf("%c", str);

		fgets(str, 100, stdin);
		fputs(str, f);
		fclose(f);
	}
}
void frd(char* str)
{
	FILE *f;
	if ((f = fopen("D:\\text.txt", "r+t")) == NULL)
		printf("Файл невозможно открыть \n");
	else
	{
		fgets(str, 100, f);
		puts(str);
		fclose(f);
	}
}
void enc(char* str, char* key)
{
	int i, len;
	char c = '1';
	len = strlen(str);

	FILE *f;
	if ((f = fopen("D:\\text.txt", "w+t")) == NULL)
		printf("Файл невозможно создать \n");
	else
	{
		fgets(str, 100, f);

		for (i = 0; i < len; i += 2)
			(key[i] = str[i]) && (str[i] = c);

		fputs(str, f);
		printf("Содержимое файла зашифровано \n");
		puts(str);

		fclose(f);
	}
}
void dec(char* str, char* key)
{
	int i, len;
	char c = '1';
	len = strlen(str);

	FILE *f;
	if ((f = fopen("D:\\text.txt", "w+t")) == NULL)
		printf("Файл невозможно открыть \n");
	else
	{
		fgets(str, 100, f);

		for (i = 0; i < len; i += 2)
		{
			if (str[i] = c)
			{
				str[i] = key[i];
			}
		}
	}
	fputs(str, f);
	printf("Содержимое файла расшифровано \n");
	puts(str);

	fclose(f);
}
