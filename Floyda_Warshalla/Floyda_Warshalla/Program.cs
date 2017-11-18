using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floyda_Warshalla
{
    class Program
    {
        const short BrakDrogi = short.MaxValue;
        //deklaracja macierzy
        static int[,] MacierzWag; //macierz Wag/Sąsiedztwa
        static int[,] MacierzOdległości;
        static int[,] MacierzWęzłówPośrednich;


        static void Main(string[] args)
        {//deklaracje pomocnicze
            int n; //liczba wierzchołków grafu
            int i, j, k; //indeksy dla macierzy
            string Input; //ciąg znaków wczytywany z klawiatury
            //metryka programu
            Console.WriteLine("\n\tProgram: Algorytm_Floyda_Warshalla wyznacza najkrótsze ścieżki \n\t" + " (drogi) między węzłami grafu");
            //kolejność danych wyjściowych: n (liczba węzłów), macierz wag wierszami
            //warunek wejściowy: n > 0
            do
            {
                Console.Write("Podaj liczbę węzłów grafu (n > 0): ");
                while (!int.TryParse(Console.ReadLine(), out n))
                {//sygnalizacja błędu
                    Console.WriteLine("\n\tERROR: Wystąpił niedozwolony znak w zapisie liczby węzłów grafu ");
                    Console.WriteLine("\n\tMasz kolejną szansę: wprowadź tą liczbę jeszcze raz!");

                };
                //sprawdzenie warunku wyjściowego
                if (n <= 0)
                {
                    Console.WriteLine("\n\tERROR: Liczba węzłów w grafie musi spełniać warunek: n>0 ");
                    Console.WriteLine("\n\tMasz kolejną szansę i skorzystaj z niej!");
                }


            } while (n <= 0);
            //Utworzenie egzemplarzy macierzy opisu grafu
            MacierzWag = new int[n, n];
            MacierzOdległości = new int[n, n];
            MacierzWęzłówPośrednich = new int[n, n];

            //wczytanie macierzy wag/sąsiedztwa
            Console.WriteLine("\n\tWczytywanie elementów macierzy wag (wierszami): ");
            Console.WriteLine("\n\t(jeżeli między podanymi numerami wierzchołków grafu nie ma " + " drogi bezpośredniej to naciśnij ENTER");
            for (j = 0; j < MacierzWag.GetLength(0); j++){
                for (i = 0; i < MacierzWag.GetLength(1); i++)
                {
                    if (i == j)
                    {
                        MacierzWag[j, i] = 0;
                    }
                    else
                    {
                        Console.Write("\n\tPodaj wagę dla krawędzi między węzłami ({0}, {1}): ", j, i);
                        Input = Console.ReadLine();
                        // usuwamy przypadkowe spacje
                        Input = Input.Trim();
                        if (Input.Equals(""))
                        {
                            MacierzWag[j, i] = BrakDrogi; //był naciśnięty klawisz enter
                            break;
                        }
                        else
                        {
                            while (!int.TryParse(Input, out MacierzWag[j, i]))
                            {
                                Console.WriteLine("\n\tERROR: W zapisie podanej wagi krawędzi grafu wystąpił" + " niedozwolony znak!");
                                Console.Write("\n\tPodaj tą wagę jeszcze raz: ");
                                //wczytanie nowej wagi
                                Input = Input.Trim();
                                
                            }
                        }
                    }

                }
        }

            //chwilowe zatrzymanie programu
            Console.WriteLine("\n\tDla kontynuajcji działania programu, naciśnij dowolny klawisz!");
            Console.ReadKey();
            // kontrolne wypisanie wczytanej macierzy wag
            Console.WriteLine("\n\tkontrolne wpisanie wczytanej macierzy wag");
            for (j = 0; j < MacierzWag.GetLength(0); j++)
            {
                Console.Write("\t");
                for (i = 0; i < MacierzWag.GetLength(1); i++)
                {//wypisujemy elementy j-tego wiersza macierzy wag
                    if (MacierzWag[j, i] == BrakDrogi)
                        Console.Write(" {0:3} ", "*");
                    else
                        Console.Write(" {0:3} ", Convert.ToString(MacierzWag[j, i]));
                }
                Console.WriteLine(); /*przejście do nowej linii dla wypisania kolejnego wiersza macierzy wag */
            }

            //chwilowe zatrzymanie programu
            Console.WriteLine("\n\tDla kontynuajcji działania programu, naciśnij dowolny klawisz!");
            Console.ReadKey();

            //Algorytm_Floyda_Warshalla
            //skopiowanie macierzy wag do MacierzyOdległości

            for (j = 0; j < MacierzWag.GetLength(0); j++) { 
                for (i = 0; i < MacierzWag.GetLength(1); i++){
                    MacierzOdległości[j, i] = MacierzWag[j, i];
                }
            }
            //wyzerowanie macierzy węzłów pośrednich
            for (j = 0; j < MacierzWęzłówPośrednich.GetLength(0); j++)
            {
                for (i = 0; i < MacierzWęzłówPośrednich.GetLength(1); i++)
                {
                    MacierzWęzłówPośrednich[j, i] = 0;
                }
            }

            //algorytm wyszukiwania najkrótszych dróg w grafie
            for (k = 0; k < MacierzWęzłówPośrednich.GetLength(0); k++)
            {
                for (j = 0; j < MacierzOdległości.GetLength(0); j++) {
                    for (i = 0; i < MacierzOdległości.GetLength(1); i++)
                    {
                        if ((MacierzOdległości[j, k] + MacierzOdległości[k, i]) < MacierzOdległości[j, i])
                        
                            MacierzOdległości[j, i] = MacierzOdległości[j, k] + MacierzOdległości[k, i];
                        //zapamiętanie numeru węzła pośredniego dla przejścia od węzła j do i 
                        MacierzWęzłówPośrednich[j, i] = k;
                    }
                    }
            }
            
        

            //wypisanie macierzy najkrótszych ścieżek między węzłami grafu
            for (j = 0; j < MacierzOdległości.GetLength(0); j++)
            {
                Console.Write("\t");
                for (i = 0; i < MacierzOdległości.GetLength(1); i++)
                {//wypisujemy elementy j-tego wiersza macierzy ścieżek
                    if (MacierzOdległości[j, i] == BrakDrogi)
                        Console.Write(" {0:3} ", "*");
                    else
                        Console.Write(" {0:3} ", Convert.ToString(j), Convert.ToString(i));
                }
                Console.WriteLine(); /*przejście do nowej linii dla wypisania kolejnego wiersza macierzy wag */
            }
            //wypisanie macierzy najkrótszych ścieżek między węzłami grafu
            for (j = 0; j < MacierzOdległości.GetLength(0); j++)
            {
                Console.Write("\t");
                for (i = 0; i < MacierzOdległości.GetLength(1); i++)
                
                {//wypisujemy elementy j-tego wiersza macierzy ścieżek
                    if (MacierzWęzłówPośrednich[j, i] == 0)
                        Console.Write(" {0:3} ", "*");
                    else
                        Console.Write(" {0:3} ", Convert.ToString(MacierzWęzłówPośrednich[j, i]));
                }//chwilowe zatrzymanie programu
                Console.WriteLine("\n\tDla kontynuajcji działania programu, naciśnij dowolny klawisz!");
                Console.ReadKey(); 

            }
        }
    }
}