using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DAL.Data.Models;
using DAL.Data.Models.Movie_Module;
using DAL.Data.Repositories.Intrfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Repositories.Calsses
{
    public class DataSeeding(
            AppDbContext _dbContext,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();

            try
            {
                #region Seed Local Data

                if (!_dbContext.Set<Actor>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\actors.json");
                    var data = await JsonSerializer.DeserializeAsync<List<Actor>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.Actors.AddRangeAsync(data);

                }

                if (!_dbContext.Set<Cinema>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\cinemas.json");
                    var data = await JsonSerializer.DeserializeAsync<List<Cinema>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.Cinemas.AddRangeAsync(data);
                }

                if (!_dbContext.Set<Movie>().Any())
                {
                    // 1. Configure JSON serializer options
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new JsonStringEnumConverter() } // Handles enum conversions
                    };

                    // 2. Use proper path resolution
                    var jsonPath = Path.Combine(Directory.GetCurrentDirectory(),
                                              @"..\DAL\Data\DataSeed\movie.json");

                    try
                    {
                        // 3. Read and validate JSON structure first
                        var jsonString = await File.ReadAllTextAsync(jsonPath);
                        if (string.IsNullOrWhiteSpace(jsonString))
                        {
                            throw new InvalidDataException("Movie JSON file is empty");
                        }

                        // 4. Deserialize with proper error handling
                        var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(
                            new MemoryStream(Encoding.UTF8.GetBytes(jsonString)), options);

                        if (movies == null || !movies.Any())
                        {
                            Console.WriteLine("No valid movies found in JSON file");
                            return;
                        }

                        // 5. Validate each movie before saving
                        var validMovies = new List<Movie>();
                        foreach (var movie in movies)
                        {
                            try
                            {
                                ValidateMovie(movie);
                                validMovies.Add(movie);
                            }
                            catch (ValidationException ex)
                            {
                                Console.WriteLine($"Skipping invalid movie: {ex.Message}");
                                // Consider logging to a file or monitoring system
                            }
                        }

                        // 6. Save only valid movies
                        if (validMovies.Any())
                        {
                            await _dbContext.Movies.AddRangeAsync(validMovies);
                            await _dbContext.SaveChangesAsync();
                            Console.WriteLine($"Successfully seeded {validMovies.Count} movies");
                        }
                        else
                        {
                            Console.WriteLine("No valid movies to seed after validation");
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON Error (Line {ex.LineNumber}, Pos {ex.BytePositionInLine}): {ex.Message}");
                        throw; // Re-throw to fail fast if this is critical
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error seeding movies: {ex.Message}");
                        throw;
                    }
                }

                if (!_dbContext.Set<MovieActor>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\movieActor.json");
                    var data = await JsonSerializer.DeserializeAsync<List<MovieActor>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.MovieActors.AddRangeAsync(data);
                }

                if (!_dbContext.Set<Producer>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\producers.json");
                    var data = await JsonSerializer.DeserializeAsync<List<Producer>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.Producers.AddRangeAsync(data);
                }

                if (!_dbContext.Set<ShowTime>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\showtime.json");
                    var data = await JsonSerializer.DeserializeAsync<List<ShowTime>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.ShowTimes.AddRangeAsync(data);
                }

                if (!_dbContext.Set<Timing>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\Timings.json");
                    var data = await JsonSerializer.DeserializeAsync<List<Timing>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.Timings.AddRangeAsync(data);
                }

                if (!_dbContext.Set<Ticket>().Any())
                {
                    var stream = File.OpenRead(@"..\DAL\Data\DataSeed\tickets.json");
                    var data = await JsonSerializer.DeserializeAsync<List<Ticket>>(stream);
                    if (data is not null && data.Any())
                        await _dbContext.Tickets.AddRangeAsync(data);
                }

                #endregion

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data seed error: {ex.Message}");
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                var jsonPath = @"..\DAL\Data\DataSeed\users.json";

                if (!File.Exists(jsonPath))
                {
                    Console.WriteLine("User seed file not found.");
                    return;
                }

                var jsonData = await File.ReadAllTextAsync(jsonPath);
                var users = JsonSerializer.Deserialize<List<UserSeedModel>>(jsonData);

                if (users is null || !users.Any())
                {
                    Console.WriteLine("No user data to seed.");
                    return;
                }

                // Create roles
                var uniqueRoles = users
                    .Select(u => u.Role)
                    .Where(r => !string.IsNullOrWhiteSpace(r))
                    .Distinct();

                foreach (var role in uniqueRoles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Create users and assign roles
                foreach (var userSeed in users)
                {
                    var existingUser = await _userManager.FindByEmailAsync(userSeed.Email);
                    if (existingUser == null)
                    {
                        var newUser = new ApplicationUser
                        {
                            Email = userSeed.Email,
                            UserName = userSeed.UserName,
                            FullName = userSeed.FullName,
                            PhoneNumber = "0000000000"
                        };

                        var createResult = await _userManager.CreateAsync(newUser, userSeed.Password);
                        if (createResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, userSeed.Role);
                            Console.WriteLine($"Seeded user: {newUser.Email}");
                        }
                        else
                        {
                            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                            Console.WriteLine($"Failed to seed user {newUser.Email}: {errors}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during identity seeding: {ex.Message}");
            }
        }
        private void ValidateMovie(Movie movie)
        {
            if (movie == null)
                throw new ValidationException("Movie object is null");

            if (string.IsNullOrWhiteSpace(movie.Name))
                throw new ValidationException($"Movie name is required (ID: {movie.Id})");

            if (string.IsNullOrWhiteSpace(movie.Description))
                throw new ValidationException($"Description is required for {movie.Name}");

            if (!Enum.IsDefined(typeof(Category), movie.MovieCategory))
                throw new ValidationException($"Invalid category '{movie.MovieCategory}' for {movie.Name}");

            if (movie.ProducerId <= 0)
                throw new ValidationException($"Invalid ProducerId for {movie.Name}");

            if (string.IsNullOrWhiteSpace(movie.ImageURL))
                throw new ValidationException($"ImageURL is required for {movie.Name}");
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }
    }
}
