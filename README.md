# ChatGPT Buddy

Written in C#, this program houses code to communicate with ChatGPT's API. It is
a simple way to keep track of conversations with ChatGPT and has a few options
to customize usage.

# Getting Started

This application requires .NET 6, dotnet-ef CLI tools, and a SQLite database
file.

## Environment Variables

*Coming soon*

For now set the API key (apiKey) in `Program.cs` and the database path (where
.UseSqlite appears) in `DataContext.cs`.

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