﻿using DiscographyViewerAPI.Models.ViewModels;
using SpelarDuInAPIClient.Methods;
using SpelarDuInAPIClient.Models;
using SpelarDuInClient.Methods;
using System.Text.Json;

namespace SpelarDuInAPIClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {

                // Connecting to the API

                client.BaseAddress = new Uri("https://localhost:7107/");  // Connecting to the API

            repeat:

                await Console.Out.WriteLineAsync("Input [1] to list users or [2] create new user:");

                string response = Console.ReadLine();

                int userId = 0;
                UserViewModel selectedUser = null;

                UserViewModel selectedUser = null;

                // Lists all users

                if (response == "1")
                {
                    await UserMethods.ListAllUsersAsync(client);

                    await Console.Out.WriteLineAsync("Select your ID.");

                    string strUserId = Console.ReadLine();

                    userId = Convert.ToInt32(strUserId);

                    selectedUser = await UserMethods.SelectUserAsync(client, userId);

                }

                // Create new user
                else if (response == "2")
                {
                    await UserMethods.CreateNewUserAsync(client);

                    goto repeat;
                }
                else if (response == null)
                {
                    await Console.Out.WriteLineAsync("Ya doin something wrong");
                }

                while (true)
                {

                    await Console.Out.WriteLineAsync($"Hello {selectedUser.UserName}! Please Enter 1 to add genre, 2 to list your genres, 3 to add artist, 4 to list your artists, 7 to list all artists: ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": // AddGenre

                            await GenreMethods.AddGenreAsync(client);

                            break;
                        case "2": // List Genres

                            await GenreMethods.ListUserGenresAsync(client, selectedUser.Id);

                            break;
                        case "3": // Add new artist

                            await ArtistMethods.AddNewArtistAysnc(client);

                            break;

                        case "4": // list artists related to a specific user
                            await ArtistMethods.ListUserArtistsAsync(client, selectedUser.Id);

                            break;

                        case "5": // add track
                            await ClientTrackHandler.AddtrackAsync(client);
                            break;

                        case "6": // list track
                            await ClientTrackHandler.GetAlltracksFromSingleUserAsync(client, userId);
                            break;

                        case "7": //list all artists
                            await ArtistMethods.ListAllArtistsAsync(client);
                            break;

                        //case "8":
                        //    await ArtistMethods.ViewAnArtistAsync(client, artistName);
                        //    break;

                        default:
                            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");

                            break;
                    }

                }

            }
        }
    }
}
