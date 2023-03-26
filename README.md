# ChatGPT Buddy

Written in C#, this program houses code to communicate with ChatGPT's API. It is
a simple way to keep track of conversations with ChatGPT and has a few options
to customize usage.

# Getting Started

This application requires .NET 6, dotnet-ef CLI tools, and a SQLite database
file.

## Environment Variables

Without the environment variables set, the application will exit after giving
a notice in the terminal.

CGB_API_KEY - required, sets the API key for your OpenAI account. Without this
the program can't communicate with the ChatGPT API.

CGB_DB_CONNECTION - required, sets the path to your SQLite database file.

## Running the Project

Run the migrations, a SQLite file should be made where you specified previously:

``` bash
dotnet restore
dotnet-ef database update --verbose # remember to install dotnet-ef!
```

Then, run the program to start chatting!

``` bash
dotnet run
```

## License

MIT License Copyright (c) 2023 jlprince21

## Contributing

Send a pull request if interested in contributing. If my interest keeps up, I
will add some stuff to the Issues board maybe, who knows.

## Credits

Cheers to the person who snagged "OpenAI" for their NuGet package which I
happened to use for this project's code lol.
[GitHub](https://github.com/OkGoDoIt/OpenAI-API-dotnet)