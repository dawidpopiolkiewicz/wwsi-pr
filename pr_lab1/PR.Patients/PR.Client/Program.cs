using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PR.Client
{
    class Program
    {

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            int option;
            do
            {

                Console.WriteLine("\n*** System COVID-19 ***");
                Console.WriteLine("\n1. Dodaj pacjenta");
                Console.WriteLine("2. Lista pacjentów");
                Console.WriteLine("\n9. Wyjście");

                option = Int32.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\nPodaj imię:");
                        var FirstName = Console.ReadLine();
                        Console.WriteLine("Podaj nazwisko:");
                        var LastName = Console.ReadLine();
                        Console.WriteLine("Podaj wiek:");
                        var Age = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Podaj email:");
                        var Email = Console.ReadLine();

                        var patient = new Patients.Model.Patients()
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            Age = Age,
                            TestDate = DateTime.Now,
                            Email = Email
                        };

                        Console.WriteLine("\nDodawanie pacjenta w toku ...");


                        string userJson = JsonSerializer.Serialize(patient);
                        var httpResponseMessage = await client.PostAsync("https://localhost:5001/api/patients", new StringContent(userJson, Encoding.UTF8, "application/json"));
                        if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            Console.WriteLine("\nPacjent został dodany");
                        }
                        else
                        {
                            Console.WriteLine("\nBłąd podczas dodawania pacjenta: " + httpResponseMessage.StatusCode);
                        }
                        break;
                    case 2:
                        Console.WriteLine("\nLista pacjentów:");
                        var response = await client.GetAsync("https://localhost:5001/api/patients");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var patientList = JsonSerializer.Deserialize<List<Patients.Model.Patients>>(responseBody);
                        var number = 0;
                        foreach (var p in patientList)
                        {
                            number++;
                            Console.WriteLine("\nNr: " + number + "\nImię: " + p.FirstName + ",\nNazwisko: " + p.LastName + ",\nWiek: " + p.Age + ",\nData testu: " + p.TestDate + ",\nEmail: " + p.Email);
                        }

                        break;
                    case 9:
                        Console.WriteLine("wybrano 9 - koniec programu");
                        break;
                    default:
                        Console.WriteLine("Wybrano błędną opcję. Spróbuj raz jeszcze");
                        break;
                }


            } while (option != 9);

        }
    }
}
