using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using notKaydi.Model;
using System.Data.SqlClient;
using System.Data;

namespace notKaydi
{
    class dosyaIzleme
    {
        public void Dinle()
        {
            FileSystemWatcher fsw = new FileSystemWatcher();

            fsw.Path = @"izlenecek klasor yolu-------------";
            fsw.Filter = "*.csv";
            fsw.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName;
            //fsw.Changed += new FileSystemEventHandler(Degisiklik);
            fsw.Created += new FileSystemEventHandler(Olusturma);
            //fsw.Deleted += new FileSystemEventHandler(Silinme);
            //fsw.Renamed += new RenamedEventHandler(IsimDegisikligi);
            fsw.IncludeSubdirectories = false;
            fsw.EnableRaisingEvents = true;
        }
        private contextsS db = new contextsS();
        public void Olusturma(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Veriler veritabanına aktarılıyor...");
            string metin;
            dersler drs = new dersler();
            nots nts = new nots();
            String Filename = @"C:\Users\Coder\Desktop\notKaydi\notKaydi\notlar\" + Path.GetFileName(e.FullPath);
            
            string[] cellValue;

            if (System.IO.File.Exists(Filename))
            {
                int sayac = 0;
                int tut=0;
                StreamReader streamReader = new StreamReader(Filename);
                DataTable tablo = new DataTable();
                DataTable tablo2 = new DataTable();
                while ((metin = streamReader.ReadLine()) != null)
                {
                    if (sayac>0)
                    {
                        cellValue = metin.Split(',');
                        if (cellValue.Count()==6)
                        {
                            string VeritataniAdi = "notkayit";
                            SqlConnection baglanti = new SqlConnection("server=.\\SQLEXPRESS;database=Master; Integrated Security=SSPI");
                            SqlCommand komut = new SqlCommand("SELECT Count(name) FROM master.dbo.sysdatabases WHERE name=@prmVeritabani", baglanti);
                            komut.Parameters.AddWithValue("@prmVeriTabani", VeritataniAdi);
                            baglanti.Open();
                            int sonuc = (int)komut.ExecuteScalar();
                            baglanti.Close();
                            if (sonuc != 0)
                            {
                                SqlConnection baglan = new SqlConnection("server=DELL\\SQLEXPRESS;initial catalog=notkayit; integrated security=true");
                                baglan.Open();
                                string cumle = "select * from derslers where dersAdi=@adi";
                                SqlParameter prmtr1 = new SqlParameter("@adi", cellValue[0]);
                                SqlCommand varmi = new SqlCommand(cumle, baglan);
                                varmi.Parameters.Add(prmtr1);
                                SqlDataAdapter adapter = new SqlDataAdapter(varmi);
                                adapter.Fill(tablo);

                                if (tablo.Rows.Count == 0)
                                {
                                    drs.dersAdi = cellValue[0];
                                    db.derslers.Add(drs);
                                    db.SaveChanges();
                                    adapter.Fill(tablo);
                                    tut = Convert.ToInt16(tablo.Rows[0][0]);
                                }
                                else
                                {
                                    tut = Convert.ToInt16(tablo.Rows[0][0]);
                                }
                                tablo.Clear();
                                baglan.Close();
                            }
                            else
                            {
                                drs.dersAdi = cellValue[0];
                                db.derslers.Add(drs);
                                db.SaveChanges();
                                tut = drs.dersID;
                                nts.dersID = tut;
                                nts.ogrenciID = Convert.ToInt16(cellValue[1]);
                                nts.vize1 = Convert.ToInt16(cellValue[2]);
                                nts.vize2 = Convert.ToInt16(cellValue[3]);
                                nts.vize3 = Convert.ToInt16(cellValue[4]);
                                nts.final = Convert.ToInt16(cellValue[5]);

                                db.notss.Add(nts);
                                db.SaveChanges();
                            }

                            if (sonuc != 0)
                            {
                                SqlConnection baglan2 = new SqlConnection("server=DELL\\SQLEXPRESS;initial catalog=notkayit; integrated security=true");
                                baglan2.Open();

                                string cumle3 = "select dersID,ogrenciID from nots where dersID=(select dersID from derslers where dersAdi=@drs) and ogrenciID=@ogrid";
                                SqlParameter prmtr8 = new SqlParameter("@drs", cellValue[0]);
                                SqlParameter prmtr9 = new SqlParameter("@ogrid", cellValue[1]);
                                SqlCommand varmi2 = new SqlCommand(cumle3, baglan2);
                                varmi2.Parameters.Add(prmtr8);
                                varmi2.Parameters.Add(prmtr9);
                                SqlDataAdapter adapter2 = new SqlDataAdapter(varmi2);
                                adapter2.Fill(tablo2);

                                if (tablo2.Rows.Count==0)
                                {
                                    nts.dersID = tut;
                                    nts.ogrenciID = Convert.ToInt16(cellValue[1]);
                                    nts.vize1 = Convert.ToInt16(cellValue[2]);
                                    nts.vize2 = Convert.ToInt16(cellValue[3]);
                                    nts.vize3 = Convert.ToInt16(cellValue[4]);
                                    nts.final = Convert.ToInt16(cellValue[5]);
                                    db.notss.Add(nts);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    string cumle2 = "update nots set vize1=@v1,vize2=@v2,vize3=@v3,final=@f1  where dersID=(select dersID from derslers where dersAdi=@drs) and ogrenciID=@ogrid";
                                    SqlParameter prmtr2 = new SqlParameter("@drs", cellValue[0]);
                                    SqlParameter prmtr3 = new SqlParameter("@ogrid", cellValue[1]);
                                    SqlParameter prmtr4 = new SqlParameter("@v1", cellValue[2]);
                                    SqlParameter prmtr5 = new SqlParameter("@v2", cellValue[3]);
                                    SqlParameter prmtr6 = new SqlParameter("@v3", cellValue[4]);
                                    SqlParameter prmtr7 = new SqlParameter("@f1", cellValue[5]);
                                    SqlCommand kayyit = new SqlCommand(cumle2, baglan2);
                                    kayyit.Parameters.Add(prmtr2);
                                    kayyit.Parameters.Add(prmtr3);
                                    kayyit.Parameters.Add(prmtr4);
                                    kayyit.Parameters.Add(prmtr5);
                                    kayyit.Parameters.Add(prmtr6);
                                    kayyit.Parameters.Add(prmtr7);
                                    kayyit.ExecuteNonQuery();
                                }
                                tablo.Clear();
                                baglan2.Close();
                            }
                            else
                            {

                            }
                        }
                    }
                    sayac++;
                }
                streamReader.Close();
                File.Delete(Filename);
                
            }
            Console.WriteLine("İşlem Bitmiştir");
        }
    }
}
