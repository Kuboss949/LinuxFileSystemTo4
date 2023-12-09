using System;
using LinuxFileSystemTo4.Composite;

namespace LinuxFileSystemTo4.FileSystem;
using Directory = LinuxFileSystemTo4.Composite.Directory;

public class FileSystem
{
    private Directory rootDirectory;

    public Directory RootDirectory
    {
        get => rootDirectory;
        set => rootDirectory = value ?? throw new ArgumentNullException(nameof(value));
    }

    public FileSystem()
    {
        this.BuildFileSystem();
    }

    private void BuildFileSystem()
    {
        Directory d;
        string[] nameList = {"root", "etc", "home", "images", "music"};
        d = new Directory("root");
        this.rootDirectory = d;
        d.AddFile(new Directory("etc"));
        d.AddFile(new Directory("home"));
        d.AddFile(new TextFile("hi.txt", "1233123"));
        d = (Directory)d.GetFile("home");
        d.AddFile(new Directory("images"));
        d.AddFile(new Directory("music"));
        d.AddFile(new TextFile("legenda.txt", @"Dawno, dawno tymu, za siedmioma #!$%@?, za siedmioma serkami, w łokolicach Gubałówki mieszkał Tomisław Apolonius Curuś Bachleda Farrell, jak ten piecyk z dmuchawą. Pewnego dnia Tomisław wysedł na halę i pomyśloł ""Krucafuks, dość! Ile mozna #!$%@?ć łoscype!. Ani to dobre, ani śwarne, a jakie drogie #!$%@?! No i sracka murowana. Toć to gołymi łapami ługniotane! Zeby to jesce z krowiego mleka było, a weź tu łodróznij barana łod owiecki.. stąd ten słonawy posmak..""
- Tfuuuu.. łohydne. Co te turysty w tym widzą.
Podcas gdy Bachleda zajodał się łoscypkiem, smród baraniego nabiału niósł się po łokolicy, nawet pod wiater #!$%@?ło napletkiem, hej. Bekasy nie wytrzymały, fetor przegonił je z terenów lęgowych. Ryby w Morskim Oku zdechły,a w Carnym Stawie zdechły. Niedźwiedź Gąsiennica łobudził się ze snu zimowego i łodrazu poszedł pod prysznic, lecz to nie tylko od niego tak #!$%@?ło. Niby po kąpieli smród bełta i gówna ustąpił, ale pses to woń łoctówy stała się jeszcze bardziej nieznośna. Lecz specyficzny zapach łobudził nie tyko misia, łobudził tez żądze, łobzydliwe, homoseksualne żądze. Śniezny kockodan, pzez niektórych nazywany tez yeti dostaje smergla gdy pocuje woń fiuta, hej. Tak się składo, że łoscypek pachnie tożsamo. Koniec.
-Dziadku Tomisławie, cemu legendę o śniegowym kockodanie zawsze przerywas w połowie, hej?
-Co mos na myśli, hej?
-Nie łopisujes swojego spotkania z yeti, hej.
-Kruca! Skąd wies, że ta historyjka jest ło mnie?
-Tomisław Łapolonius Curuś Bachleda Farrel, są niewielkie szanse żeby druga łosoba nazywała się tak idiotycnie. Dziadku.. nie chce powtazać plotek, ale..
-Godej co godajo w Murzasichle!
-Ehh..
Śniezny kockodan, pzez niektórych nazywany tez yeti dostaje śmergla gdy pocuje woń fiuta. Tak się składo, że łoscypek pachnie tożsamo. Bestia normalnie zywi się łowocami, ale łocet budzi w niej zbocenie. Tamtej nocy zesła z wierchu na halę wiedziona zapachem rozporka. Nieświadomy nicego Bachleda Farrell zajadoł właśnie syr. Pirse razy posły w dupe, hej. Litl boj małpy rozdupcył Hirosime Bachledy. Potem kockodan załozył strapona Big Ben i dzwonił jajami o brodę Curusia. Kosmar małpy trwał kilka dni, Bachleda nie chciał jej puścić, donosił tylko spsęty, łod których małpie robiły się wielkie łocy. Yeti nie wytrzymoł gdy Bachleda przyniósł słoik, a gdy słój pękł, a Bachleda zabronił wzywać karetkę tylko nakazoł nagrywać dalej małpa zwariowała. I grasuje po okolicy po dziś dzień, a film ze słoikiem dostępny jest w sieci. Koniec."));
        d = (Directory)d.GetFile("images");
        d.AddFile(new Directory("subimages"));
    }
    
}