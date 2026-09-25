# CarWashManager

WPF rakendus autopesula tellimuste haldamiseks

## Projekti struktuur

- CarWashManager.Core - arvutused ja ärireeglid
- CarWashManager.WpfApp - WPF kasutajaliides

## Funktsioonid

- Tellimuste lisamine
- Tellimuste muutmine
- Tellimuste kustutamine
- Tellimuste otsimine registreerimisnumbri järgi
- Tellimuste kuvamine DataGridis
- Hinna ja pesu kestuse automaatne arvutamine
- Aktiivse järjekorra kestuse arvutamine
- Eeldatava lõpetamise aja kuvamine
- Kasutajale nähtavad veateated Resources.resx failis

## Andmed

Sõiduki tüübid:

- PassengerCar
- SUV
- Van

Pesuprogrammid:

- Basic
- Standard
- Premium

Staatused:

- Waiting
- InProgress
- Completed

Tellimused salvestatakse fikseeritud 100 elemendiga massiivi

## Eeldused

- Registreerimisnumber peab sisaldama 3 kuni 10 märki
- Completed staatusega tellimust ei arvestata aktiivse järjekorra kestuses
- Hind ja kestus sõltuvad sõiduki tüübi ja pesuprogrammi kombinatsioonist
- Eeldatav lõpetamise aeg arvutatakse aktiivse järjekorra kogukestuse põhjal

## Kontrollnäited

### Näide 1

PassengerCar + Basic

- Hind: 10 €
- Kestus: 15 min

### Näide 2

SUV + Premium

- Hind: 24 €
- Kestus: 40 min

### Näide 3

Van + Standard

- Hind: 22 €
- Kestus: 35 min

## Käivitamine

1. Ava CarWashManager solution Visual Studios
2. Määra CarWashManager.WpfApp käivitusprojektiks
3. Käivita rakendus
