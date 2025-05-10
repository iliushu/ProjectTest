// BooksAdd.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include <string>
using namespace std;
struct Book
{
    char* title;
	char* author;
    int year;
};

class Library {
public:
    Library() :books(nullptr), size(0), capacity(5)
    {
        books = new Book * [capacity];
    }
    void addBook(const char* title, const char* author, int year);
    void removeBook(int index);
    void displayBooks()const;
    void resize();
private:
    Book** books;
    int size;
	int capacity;

    void cleanup();
    void allocateAddCopy(Book**& destination, Book** soure, int newCapacity);
};
void Library::addBook(const char* title, const char* author, int year)
{
    if (size >= capacity) {
		resize();
	}
	Book* book = new Book;
    book->title = new char[strlen(title) + 1];
    book->author = new char[strlen(author) + 1];
    strcpy(book->title, title);
    strcpy(book->author, author);
    book->year = year;
    books[size++] = book;
}
void Library::removeBook(int index)
{
    if (index < 0 || index >= size)return;
    delete[] books[index]->title;
    delete[] books[index]->author;
    delete books[index];
    for (int i = index; i < size - 1; i++)
    {
        books[i] = books[i + 1];
    }
    --size;
}
void Library::displayBooks()const
{
    for (int i = 0;i < size;++i)
    {
		cout << "Title: " << books[i]->title << ", Author: " << books[i]->author << ", Year: " << books[i]->year << endl;
    }
}
void Library::resize()
{
    int newCapactiy = capacity * 2;
    allocateAddCopy(books, books, newCapactiy);
    capacity = newCapactiy;
}
void Library::cleanup()
{
    for (int i = 0;i < size;++i)
    {
        delete[] books[i]->title;
        delete[] books[i]->author;
		delete books[i];
    }
    delete[] books;
}
void Library::allocateAddCopy(Book**& destination, Book** source, int newCapactiy)
{
    Book** temp = new Book * [newCapactiy];
    for (int i = 0; i < size; ++i)
    {
        temp[i] = source[i];
    }
    delete[]destination;
    destination = temp;
}
int main()
{
    std::cout << "Hello World!\n";
}

