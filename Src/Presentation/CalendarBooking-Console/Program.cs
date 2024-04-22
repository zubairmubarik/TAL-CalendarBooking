// See https://aka.ms/new-console-template for more information
//using System.Runtime.CompilerServices;
using Application.Appointments.Commands.CreateAppointments;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Common.Extensions;
using Application;
using Persistence;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Application.Appointments.Commands.DeleteAppointment;
using Application.Appointments.Queries.GetAppointmentList;

namespace CalendarBooking
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ApplicationStart();
            var services = CreateServices();

            IMediator mediator = services.GetRequiredService<IMediator>();

            while (true)
            {
                var key = UserChoice();

                switch (key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("Exit ...");
                        System.Environment.Exit(0);
                        break;
                    case ConsoleKey.A:

                        var appointment = CreateAppointment();

                        if (appointment != null)
                        {
                            var result = await mediator.Send(new CreateAppointmentCommand(appointment));
                            Console.WriteLine($"Appointment {result.Title} is booked at {result.SlotStartTime.ToStringDateTime()} " +
                                $"and Ends at {result.SlotEndTime.ToStringDateTime()}");
                            Console.ReadKey();
                        }

                        break;
                    case ConsoleKey.D:
                        var deleteSlot = DeleteAppointment();

                        if (!deleteSlot.IsMinValue())
                        {
                            var result = await mediator.Send(new DeleteAppointmentCommand(deleteSlot));
                            Console.WriteLine($"Appointment at {deleteSlot} is deleted");
                            Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.F:

                        var findSlot = FindAppointment();

                        if (!findSlot.IsMinValue())
                        {
                            var result = await mediator.Send(new GetAppointmentIEnumerableQuery(findSlot));

                            foreach (var r in result.Value)
                                Console.WriteLine($"Appointment at {r.SlotStartTime} - {r.SlotEndTime} is Available");

                            Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.K:
                        var keepAppointment = KeepAppointment();

                        if (keepAppointment != null)
                        {
                            var result = await mediator.Send(new UpdateAppointmentCommand(keepAppointment));
                            Console.WriteLine($"Appointment {result.Title} is booked at {result.SlotStartTime.ToStringDateTime()} " +
                                $"and Ends at {result.SlotEndTime.ToStringDateTime()}");
                            Console.ReadKey();
                        }

                        break;

                    default:
                        Console.WriteLine("You have pressed: " + key + "\nPress Any Key to Continue");
                        Console.ReadKey();
                        break;

                }
            }
        }

        /// <summary>
        /// Create Services nd apply DI
        /// </summary>
        /// <returns></returns>
        static ServiceProvider CreateServices()
        {

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddLogging()
                .AddApplication()
                .AddPersistence(config)
                .AddInfrastructure()
                .BuildServiceProvider();

            return serviceProvider;
        }

        static void ApplicationStart()
        {
            Console.WriteLine("This is a Demo Presentation for TAL- CalendarBooking");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();
        }

        static ConsoleKey UserChoice()
        {
            Console.Clear();
            Console.WriteLine("Welcome. Please enter your command: ");
            Console.WriteLine("Press A to Add new booking." +
                "\nPress D to Delete booking." +
                "\nPress F to Find booking." +
                "\nPress K to Keep booking." +
                "\nPress 'Esc' to Exit ");

            return (Console.ReadKey(true)).Key;
        }

        static Appointment CreateAppointment()
        {
            Console.Clear();
            Console.WriteLine("Please insert appointment title");
            string titleString = Console.ReadLine();

            Console.WriteLine("Please insert valid appointment slot in format DD/MM hh:mm (e.g 16/04 14:30)");
            string dateString = Console.ReadLine();

            // Parse the input string into a DateTime object
            DateTime parsedDate;

            //TODO: Create Client dateformat validator
            if (DateTime.TryParseExact(dateString, "dd/MM HH:mm", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                // Output the parsed DateTime object
                Console.WriteLine("Parsed Date and Time: " + parsedDate.ToString());
                return new Appointment()
                {
                    Title = titleString,
                    SlotStartTime = parsedDate,
                    IsActive = true
                };
            }
            else
            {
                Console.WriteLine("Failed to parse the input string into a DateTime object.");
                Console.ReadKey();
                return null;
            }
        }

        static DateTime DeleteAppointment()
        {
            Console.Clear();

            Console.WriteLine("Please insert valid appointment slot in format DD/MM hh:mm (e.g 16/04 14:30)");
            string dateString = Console.ReadLine();

            // Parse the input string into a DateTime object
            DateTime parsedDate;

            //TODO: Create Client dateformat validator
            if (DateTime.TryParseExact(dateString, "dd/MM HH:mm", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                // Output the parsed DateTime object
                Console.WriteLine("Parsed Date and Time: " + parsedDate.ToString());
                return parsedDate;
            }
            else
            {
                Console.WriteLine("Failed to parse the input string into a DateTime object.");
                Console.ReadKey();
                return DateTime.MinValue;
            }
        }


        static DateTime FindAppointment()
        {
            Console.Clear();

            Console.WriteLine("Please insert valid appointment slot in format DD/MM (e.g 16/04)");
            string dateString = Console.ReadLine();

            // Parse the input string into a DateTime object
            DateTime parsedDate;

            //TODO: Create Client dateformat validator
            if (DateTime.TryParseExact(dateString, "dd/MM", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                // Output the parsed DateTime object
                Console.WriteLine("Parsed Date and Time: " + parsedDate.ToString());
                return parsedDate;
            }
            else
            {
                Console.WriteLine("Failed to parse the input string into a DateTime object.");
                Console.ReadKey();
                return DateTime.MinValue;
            }
        }

        static Appointment KeepAppointment()
        {
            Console.Clear();
            Console.WriteLine("Please insert appointment title");
            string titleString = Console.ReadLine();

            Console.WriteLine("Please insert valid appointment slot in format HH:mm (e.g 14:30)");
            string dateString = Console.ReadLine();

            // Parse the input string into a DateTime object
            DateTime parsedDate;

            //TODO: Create Client dateformat validator
            if (DateTime.TryParseExact(dateString, "HH:mm", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                // Output the parsed DateTime object
                Console.WriteLine("Parsed Date and Time: " + parsedDate.ToString());
                return new Appointment()
                {
                    Title = titleString,
                    SlotStartTime = parsedDate,
                    IsActive = true
                };
            }
            else
            {
                Console.WriteLine("Failed to parse the input string into a DateTime object.");
                Console.ReadKey();
                return null;
            }
        }
    }


}


