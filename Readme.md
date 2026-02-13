# Frogobot

> A silly discord bot made for fun and our own use.

## Getting started
### Prereqs
- .NET SDK 10 installed

### Setup
1. Clone the repo:
    - `git clone <repo-url>`
2. Restore/build:
    - `dotnet restore`
    - `dotnet build`

### Secrets
If the project is run in a dev environment, it looks for secrets using dotnet user secrets.  
Otherwise, it expects secrets in environment variables.

The expected variables are:
- Discord__Token : The bot token
- DiscordEmoji__[EMOJI_NAME] : The emoji markdown format (e.g. `<:my_emoji:1234567890>:>`

## Project structure
- `Frogobot.Core/` : the bot's core logic
- `Frogobot.Data/` : everything that interacts with the bot's database
- `Frogobot.Tests/` : unit tests
