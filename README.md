# Project: GrandSlam

### Abschluss-Projekt für das Modul GraphDat / Digitale Medien SS2018 Hochschule Fulda
![alt text](http://anfuchs.de/grandslam.png "Grand Slam")
Philipp Körber, Joshua Hirsch ,Sebastian Krah, Andreas Fuchs

## Beschreibung
Erlebe „Project Grand Slam“ für den PC!

„Project Grand Slam“ ist ein, taktisches Multiplayer-Spiel für den PC. Inspiriert durch „Bomberman“, bringt „Project Grand Slam“ das bekannte Spielprinzip des “Bomben Gefechts” in die moderne Videospiel-Ära und präsentiert sich in einer verzwickten, dreidimensionalen Spielumgebung. Wie sein Vorbild, bietet es ein schnelles und chaotisches Gameplay mit zahllosen Features.
Das Spielprinzip und die Regeln sind nach wie vor die gleichen:

„Project Grand Slam“ beinhaltet einen „Battle“ Modus, in dem sich bis zu vier Spieler in einem Labyrinth wiederfinden und packenden Gefechten liefern, bis der Sieger feststeht. Steuer deinen Charakter „Slam“ durch das Spielareal, platziere deine Bomben und gehe rechtzeitig aus dem Weg, bevor du dich selbst in die Luft jagst! Nutze deine Bomben geschickt, um deine Kontrahenten auszuschalten, bevor diese es tun! Zerstöre Kisten, die dir den Weg versperren, und kämpfe dich so durch das Labyrinth!

Überliste deine Freunde mit zahlreichen, nützlichen „Power Up‘s“, welche sich in einigen zerstörbaren Kisten eines jeden Labyrinths verstecken. Mit dessen Hilfe hat dein Charakter “Slam” beispielsweise die Möglichkeit, die Reichweite der Explosion der eigenen Bombe zu erhöhen. Statte deinen Charakter mit zusätzlichen „Power Ups“ aus, um weitere Möglichkeiten zu erlangen. Dazu zählen etwa eine höhere Geschwindigkeit, die Manipulation des Gegenspielers und die Fähigkeit, Bomben fern zu zünden. Auf diese Weise gewinnen du oder deine Kontrahenten immer weitere, verheerende Fähigkeiten und treiben ihre Gegner explosiv in die Enge.

Quasi unendliche automatisch-generierte Level sorgen für Abwechslung und verhindern, dass das Spiel langweilig wird. Das Spielfeld ist zu groß oder zu klein? Es sollen mehr oder weniger Kisten auf dem Spielfeld auftauchen? Kein Problem! Über das Hauptmenü lassen sich die Level vor jedem Match nach Belieben konfigurieren und an deine Wünsche anpassen.
Steige ins Gefecht und führe deinen Charakter mit deiner Tastatur oder deinem Xbox 360 Controller in explosive Matches - aber pass auf, dass du nicht zuerst in die Luft gehst!

_____________________________________________________________________________________________________________________________

### 24.06.2018 Prototyp Version 0.02
#### 2 Player:
  * Controller oder Tastatursteuerung möglich (bisher keine Kollisionsabfrage)
  * Individuelle Werte für Speed, Leben, BombenAnzahl usw.
  * Steuerung für 4 Player und weitere Funktion vorberetet. Z.B. Play/Pause, Bomben remote Zünden.

#### 2 Items:
  * Dummy-Animation um der Spielwelt Leben einzuhauchen
  * Unterschiedlichen Eigenschaften pro Item um individuelle zuordnen zum Player zu testen
  * Rotes Item erhöht den Speed des Players (verliert der Player wieder wenn er stehen bleibt)
  * Blaues item erhöht die Stärke der Bomben des Players

#### Bomben:
  * Bomben werden mit zufälliger Ausrichtung in Y-Rotation generiert
  * Explosion stoppt an Wänden und Kisten
  * Kisten und Items werden zertört (Player noch nicht)
  * Bomben in Reichweite lösen eine Kettenreaktion aus (auch mit Bomben anderer Spieler)
  * Gelegte Bomben werden dem Player abgezogen und nach der Explosion wieder gutgeschrieben
  * Explosions Dummy um Bombenverhalten zu visualisieren

#### Map Generierung:
  * Basis Map Erzeugung
  * Zufällige Generierung von Türmen und Torböge mit unterschiedlicher Ausrichtung
  * Zufällige Verteilung von Kisten und Items
  
#### Kamera (Funktionen aktuell deaktiviert / nicht vollständig)
  * Dynamischer Zoom abhängig von der Map-Größe
  * Kamera folgt den Spielern
  * Kamera hält alle Player immer im sichtbaren Bereich
  * Hohe Entfernung: Orthogonale Ansicht im Pac-Man Style (gute Übersicht trotz geringer Elementgröße)
  * Geringe Entfernung: 3D-Ansicht mit gewollter Sichtversperrung durch Leveldesign (wie Türme und Torbögen). Player können so Bomben vor anderen Spielern versteckt platzieren oder Bomben übersehen :D.
  * Durch die dynamische Kamera verändert sich das Spielcharakter fortlaufen und ermöglicht taktischen Umgehen damit
  _____________________________________________________________________________________________________________________________

### 29.06.2018 Prototyp Version 0.04
#### Neue Levelgenerierung:
  * Level werden automatisch dynamisch aus Sectionsabschnitten zusammengebaut
  * Die Beschreibung der Muster, wird über eine Textdatei üebergeben. x = Wand. o = Gang
  * Zwischen zwei Wandstücken kann zufällig ein Torbogen generiert werden. Normal, oder um 90 Grad zur Sicht gedreht
  * Kisten werden zufällig auf den Gangstücken platziert
  * Mit den Tasten 1 - 6 kann die Menge der Kisten im Prototyp gesteuert werden

  * WorldArray ist akutell nicht mehr vorhanden
  * Aus "World" wird "LevelGenerator"
  * Das Level hat aktuell keine Information über die Breite. Die Länge wird dynamisch verändert
  * Diese beiden werden könnten daher nicht mehr für die Camera verwendet werden

#### Rigidbody auf Player_Prefab
  * Damit ist eine erste Kollisionsabfrage realisiert. Evtl. Wechsel zur Listen in Listen um direkte Zugriffe zu realisieren. Wird noch getestet
  * Davon abhängig sind die Realsierung der Explosion, Items und Kettenreaktionen zwischen Bomben und Intenraktion der Spieler

#### CameraMovement
  * Scipt zur Player-Verfolgung implementiert (Josh)
  * Aktuell aber wegen dem CameraScroller nicht funktionsfähig
  * Muss an die neue Art der LevelGenerierung und Interatkion angepasst werden
  _____________________________________________________________________________________________________________________________

### 05.08.2018 ALPHA 1
#### Levelgenerierung / -zerstörung erweitert (DistanceLine + FallScript):
  * Alle X Meter wird ein Bogen über das Spielfeld gespannt der die aktuelle Entfernung auf einem Schild anzeigt
  * Dieser Bogen passt sich procedural an den aktuellen Levelabschnitt an (Stützpfeiler und Breite, Schild immer mittig)
  * Levelelemente (Boden, Wand, Kisten, Player, Bomben) beginnen zu wackelen bevor das Level in sich zusammenbricht
  * Player kann während er wackelt von der Bodenplatte zurück in den sicheren Bereich laufen (hört auf zu wackeln)
  * Levelelemente (Boden, Wand, Kisten, Player) stürzen procedural Animiert in die Tiefe (Player stirbt)
  * Player kann nicht mehr aus dem Level laufen ohne in die Tiefe zu stürzen (Player stirbt)
  * Levelbreite erhöht

#### Sound & Musik
  * Erste Effekte und Musik implementiert um ein besseres Spielgefühl zu bekommen
  * Individueller Sound pro Spieler welcher dynamisch zugeteilt wird
  
#### Kamera
  * Depth of Field Effekt der automatisch auf die Anzahl und Position der Spieler angepasst wird
  * Fokus: Alle Player sind im Scharf
  * Colorgrading, Antialiasing, Fog, Bloom
  
#### Player
  * 4 Player implementiert + automatische Farb- und Soundzuordnung
  * Kugel (Spielfigur) hat nun eine Oberflächenstruktur und eine Rollanimation um das Spielgefühl zu verbessern
  * Dummy für Ghost-Animation wenn der Player stirbt implementiert
  
#### Bombe
  * Bomben drehen sich in zufälliger Richtung
  * Remote-Bomben erhalten automatisch die Farbe des Players der sie gelegt hat
  * Remote-Bomben zünden in der Reihenfolge in der sie gelegt wurde (können aber auch Kettenreaktionen auslösen)
  * Explosions- und Zündschnurranimation mit Particel-Systemen + Lichteffekte
  
#### Kisten
  * Explodierende Kisten werden durch eine Animation und Explosion ersetzt
  * Kisten explodieren per Unity-Physik-Engine
  
#### Sonstiges
  * Sämtliche Kollisionsabfragen selbst programmiert (Spieler, Bomben, Kisten, Welt)
  * Postprozessing
  * Particel-System unter dem Spielfeld um Bewegung und Tiefe zu vermitteln
  * Neue Texturen und Lichtstimmung
  * Performance-Verbesserungen in allen Bereichen: Code + Mesh
  
#### Steuerung PS4-Controller:
  * PS4:
  * Quadrat = Bombe
  * X =  RemoteBombe zünden
  * Steuerkreuz = Bewegen
  * Option = Restart
  * R1 = Playeranzahl +1 erhöhen
  * L1 = Playeranzahl -1 vermindern

#### Steuerung Tastatur:
  * w,s,a,d = Bewegen
  * e = Bombe
  * q = RemoteBombe zünden
  * ESC = Restart
  * 1-4 Playeranzahl
