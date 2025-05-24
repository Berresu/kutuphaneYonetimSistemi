using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2030240025_Beresu_Deniz
{
    internal class Program
    {
        public abstract class Materyal
        {
            public string Isim { get; set; }
            public string Yazar { get; set; }
            public int YayinYili { get; set; }
            public bool OduncAlindiMi { get; set; }

            public abstract void BilgileriYazdir();
        }

        public interface IOduncAlinabilir
        {
            void OduncAl();
            void OduncGeriVer();
        }

        public class Kitap : Materyal , IOduncAlinabilir
        {
            public int SayfaSayisi { get; set; }

            public override void BilgileriYazdir()
            {
                Console.WriteLine($"Kitap: {Isim}, Yazar: {Yazar}, Sayfa Sayısı: {SayfaSayisi}, Yayın Yılı: {YayinYili}");
            }

            public void OduncAl()
            {
                OduncAlindiMi = true;
                Console.WriteLine($"{Isim} İsimli Kitap Ödünç Alındı.");
            }

            public void OduncGeriVer()
            {
                OduncAlindiMi = false;
                Console.WriteLine($"{Isim} İsimli Kitap Geri Verildi.");
            }
        }

        public class Dergi : Materyal, IOduncAlinabilir
        {
            public int DergiSayisi { get; set; }

            public override void BilgileriYazdir()
            {
                Console.WriteLine($"Dergi: {Isim}, Editör: {Yazar}, Dergi Sayısı: {DergiSayisi}, Yayın Yılı: {YayinYili}");
            }

            public void OduncAl()
            {
                OduncAlindiMi = true;
                Console.WriteLine($"{Isim} İsimli Dergi Ödünç Alındı.");
            }

            public void OduncGeriVer()
            {
                OduncAlindiMi = false;
                Console.WriteLine($"{Isim} İsmli Dergi Geri Verildi.");
            }
        }

        public class Kutuphane
        {
            private List<Materyal> materyaller = new List<Materyal>();

            public void MateryalEkle(Materyal materyal)
            {
                materyaller.Add(materyal);
                Console.WriteLine("Aşağıdaki Materyaller Kütüphaneye Eklendi.");
                materyal.BilgileriYazdir();
            }

            public void MateryalleriListele()
            {
                Console.WriteLine("Materyallerin Listesi:");
                foreach(var m in materyaller)
                {
                    m.BilgileriYazdir();
                }
            }

            public void MateryalOduncAl(string isim)
            {
                foreach(Materyal materyal in materyaller)
                {
                    if(materyal.Isim == isim)
                    {
                        if(materyal is IOduncAlinabilir)
                        {
                            if(materyal.OduncAlindiMi == true)
                            {
                                Console.WriteLine("Ödünç Almak İstediğiniz Materyal Başka biri Tarafından Ödünç Alınmış.");
                            }
                            else
                            {
                                ((IOduncAlinabilir)materyal).OduncAl();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bu Materyal Ödünç Alınamaz.");
                        }

                        return;
                    }
                }
            }

            public void MateryalOduncGeriVer(string isim)
            {
                foreach (Materyal materyal in materyaller)
                {
                    if (materyal.Isim == isim)
                    {
                        if (materyal is IOduncAlinabilir)
                        {
                            if (materyal.OduncAlindiMi == false)
                            {
                                Console.WriteLine("Ödünç Vermek İstediğiniz Materyal Başka biri Tarafından Ödünç Geri Verilmiş.");
                            }
                            else
                            {
                                ((IOduncAlinabilir)materyal).OduncGeriVer();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bu Materyal Ödünç Geri Verilemez.");
                        }

                        return;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Kitap kitap1 = new Kitap() { Isim = "İstanbul Hatırası", Yazar = "Ahmet Ümit", YayinYili = 2010, SayfaSayisi = 632 };
            Dergi dergi1 = new Dergi() { Isim = "KafkaOkur", Yazar = "Alparslan Demir", YayinYili = 2024, DergiSayisi = 100 };

            Kutuphane kutuphane = new Kutuphane();

            kutuphane.MateryalEkle(kitap1);
            kutuphane.MateryalEkle(dergi1);

            kutuphane.MateryalleriListele();

            kutuphane.MateryalOduncAl("İstanbul Hatırası");
            kutuphane.MateryalOduncGeriVer("İstanbul Hatırası");

            kutuphane.MateryalleriListele();
        }
    }
}
