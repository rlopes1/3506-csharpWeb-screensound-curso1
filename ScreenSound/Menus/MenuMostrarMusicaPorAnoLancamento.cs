using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicaPorAnoLancamento: Menu<Artista>
{



    public  override void Executar(DAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);
        Console.WriteLine("Digite o ano");
        int? anoLancamento = int.Parse(Console.ReadLine());
        var musicaDAL = new DAL<Musica>(new ScreenSoundContext());
        var listaMusicas = musicaDAL.RecuperarPorAnoLancamento(x => x.AnoLancamento == anoLancamento);
     

        foreach (var musica in listaMusicas)
        {
            Console.WriteLine($"{musica}");
        }
        Console.ReadKey();
        Console.Clear();
        

    }

}
