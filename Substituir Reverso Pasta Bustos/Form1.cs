using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Substituir_Reverso_Pasta_Bustos
{
    public partial class Form1 : Form
    {
        List<String> bustosComuns = new List<String>();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (Directory.Exists(textBox1.Text)) { label1.Text = "Caminho Válido"; }
            else { label1.Text = "Caminho inválido"; }
        }


        public void substituirBustos(String caminho , List<String> listaBustos)
        {
            List<String> valorCaminhoPartes = new List<string>();
            List<String> listaAlbuns = new List<string>();
            List<String> temp = new List<string>();
            String temp2="";
            String nomeArquivo = "";
            String caminhoDestino = "";

            valorCaminhoPartes.AddRange(caminho.Split('\\')); // Pegando o nome de cada pasta separadamente
            temp2 = valorCaminhoPartes[valorCaminhoPartes.Count - 1]; //Separando o nome da última pasta
            valorCaminhoPartes.Remove(temp2); //Tirando o nome da ultima pasta, do montante de pastas
            temp2 = ""; //Limpa a string temporária

            foreach (String s in valorCaminhoPartes) //Remonta o caminho, sem a última pasta
            {
                temp2 = temp2 + s + "\\";
            }
            caminhoDestino = temp2;
            listaAlbuns.AddRange(Directory.GetDirectories(caminhoDestino,"20*")); //Lista os álbuns

            foreach (String b in listaBustos) {  //Executa a ação em cada arquivo na lista de bustos
               
                temp.Clear();
                nomeArquivo = "";
                temp.AddRange(b.Split('\\'));
                nomeArquivo = temp[temp.Count - 1];
                temp.Clear();
                //MessageBox.Show("Nome do arquivo " + nomeArquivo);

                foreach(String a in listaAlbuns){ //Executa a ação em cada álbum na lista de álbuns
                    String nomeAlbum = "";
                    temp.Clear();
                    temp.AddRange(a.Split('\\'));
                    nomeAlbum = temp[temp.Count - 1];
                    temp.Clear();

                 //   Efetuando a busca pelos arquivos da lista de bustos
                    temp.AddRange(Directory.GetFiles(a,nomeArquivo));
                  
                    foreach(String f in temp){
                        //   Tentando copiar os arquivos para a pasta de bustos
                        try {
                            if (!(Directory.Exists(caminhoDestino + "Bustos Tratados\\" + nomeAlbum)))
                            {
                                Directory.CreateDirectory(caminhoDestino + "Bustos Tratados\\" + nomeAlbum);
                            }

                            File.Copy(f, caminhoDestino + "Bustos Tratados\\" + nomeAlbum+"\\"+nomeArquivo);
                        }
                        catch { }
                    }

                }

            }
            

        }
        
        public List<String> encontrarBustos(String caminho) {
        List<String> lista = new List<string>();
        List<String> diretorios = new List<string>();
        if (Directory.Exists(caminho)) {

            diretorios.AddRange(Directory.GetDirectories(caminho,"20*"));
        
            if (diretorios.Count > 0)
            {
                foreach (String s in diretorios)
                {
                    lista.AddRange(Directory.GetFiles(s,"*.jp*"));

                }
            }
        }


        return lista;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(textBox1.Text)){
                substituirBustos(textBox1.Text, encontrarBustos(textBox1.Text));
            }
        }
    }
}
