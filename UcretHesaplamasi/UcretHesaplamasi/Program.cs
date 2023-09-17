using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcretHesaplamasi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("HOŞGELDİNİZ!");
            Console.ReadKey();
            string soru;

            do
            {
                Calisan calisan;
                TimeSpan toplamCalisma;
                string ad;
                string soyad;
                string girdigiSaat;
                string ciktigiSaat;
                double molaSaati;


                Console.WriteLine("İSİM :");
                ad = Console.ReadLine();

                Console.WriteLine("SOYİSİM :");
                soyad = Console.ReadLine();

                Console.WriteLine("GİRİŞ TARİH VE SAAT FORMAT : DD/MM/YY HH:MM :");
                girdigiSaat = Console.ReadLine();

                Console.WriteLine("ÇIKIŞ TARİH VE SAAT FORMAT : DD/MM/YY HH:MM :");
                ciktigiSaat = Console.ReadLine();

                Console.WriteLine("KAÇ SAAT MOLA YAPILDI? ");
                molaSaati = Convert.ToDouble(Console.ReadLine());

                TimeZoneInfo utcToAlmanya = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");

                //Girişi Almanya saatine döndürme
                DateTime convertedGiris = DateTime.Parse(girdigiSaat);
                convertedGiris = TimeZoneInfo.ConvertTimeFromUtc(convertedGiris, utcToAlmanya);

                //Çıkışı Almanya saatine döndürme
                DateTime convertedCikis = DateTime.Parse(ciktigiSaat);
                convertedCikis = TimeZoneInfo.ConvertTimeFromUtc(convertedCikis, utcToAlmanya);

                if (molaSaati == 0) //Mola yapmamışsa
                {
                    calisan = new Calisan(ad, soyad, convertedGiris, convertedCikis);

                    Console.WriteLine($"{calisan.İsim} {calisan.Soyisim} :");
                    Console.ReadKey();
                    Console.WriteLine($"{calisan.Giris} TARİH VE SAATİNDE GİRİŞ YAPTI");
                    Console.ReadKey();
                    Console.WriteLine($"{calisan.Cikis} TARİH VE SAATİNDE ÇIKIŞ YAPTI");
                    Console.ReadKey();

                    toplamCalisma = calisan.Cikis - calisan.Giris;

                } else
                {
                    calisan = new Calisan(ad, soyad, convertedGiris, convertedCikis, molaSaati);

                    Console.WriteLine($"{calisan.İsim} {calisan.Soyisim} :");
                    Console.ReadKey();
                    Console.WriteLine($"{calisan.Giris} TARİH VE SAATİNDE GİRİŞ YAPTI");
                    Console.ReadKey();
                    Console.WriteLine($"{calisan.Cikis} TARİH VE SAATİNDE ÇIKIŞ YAPTI");
                    Console.ReadKey();
                    Console.WriteLine($"{calisan.Mola} KADAR SAAT MOLA YAPTI");
                    Console.ReadKey();

                    toplamCalisma = calisan.Cikis - calisan.Giris;
                    toplamCalisma -= TimeSpan.FromHours(calisan.Mola); //Molayı toplamdan çıkartmak
                }

                int istenilenCalimaSaati = 8;
                double calismaUcreti = istenilenCalimaSaati * 75;

                if (toplamCalisma.TotalHours > istenilenCalimaSaati)
                {
                    double total = toplamCalisma.TotalHours;
                    int toplamMesaiSaati = Convert.ToInt32(Math.Floor(total)) - 8;
                    double mesaiUcreti = toplamMesaiSaati * 50;

                    Console.WriteLine($"TOPLAM ÇALIŞMA ÜCRETİ : {calismaUcreti} TL");
                    Console.ReadKey();

                    if (toplamMesaiSaati != 0)
                    {
                        Console.WriteLine($"TOPLAM {toplamMesaiSaati} SAAT MESAİ ÜCRETİ (SAAT BAŞINA): {mesaiUcreti} TL");
                        double toplam = calismaUcreti + mesaiUcreti;
                        Console.ReadKey();

                        Console.WriteLine($"TOPLAM ÜCRET (ÇALIŞMA + MESAİ) : {toplam}");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"TOPLAM ÇALIŞMA ÜCRETİ : {calismaUcreti} TL");
                    Console.ReadKey();

                    Console.WriteLine("MESAİ YAPILMALIŞTIR");
                    Console.ReadKey();
                }

                Console.WriteLine("BAŞKA PERSONEL İÇİN ÜCRET BİLGİSİ EDİNMEK İSTER MİSİNİZ? (evet - hayır)");
                soru = Console.ReadLine();

            } while (soru.Equals("evet"));

            Console.WriteLine("PROGRAM SONLANDIRILIYOR...");
            Console.ReadKey();
            Console.WriteLine("TEŞEKKÜR EDERİZ :)");
            Console.ReadKey();
        }
    }

    class Calisan
    {
        public string İsim { get; set; }
        public string Soyisim { get; set; }
        public DateTime Giris { get; set; }
        public DateTime Cikis { get; set; }
        public double Mola { get; set; }

        public Calisan(string isim, string soyisim, DateTime giris, DateTime cikis)
        {
            İsim = isim;
            Soyisim = soyisim;
            Giris = giris;
            Cikis = cikis;
        }

        public Calisan(string isim, string soyisim, DateTime giris, DateTime cikis, double mola)
        {
            İsim = isim;
            Soyisim = soyisim;
            Giris = giris;
            Cikis = cikis;
            Mola = mola;
        }
    }
}
