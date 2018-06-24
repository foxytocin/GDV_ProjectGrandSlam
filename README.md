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

#### 2 Items:
  * Mit unterschiedlichen Eigenschaften um zuordnen zum Player zu testen
  * Rotes Item erhöht den Speed des Players (verliert der Player wieder wenn er stehen bleibt)
  * Blaues item erhöht die Stärke der Bomben des Players

#### Bomben:
  * Bomben werden mit zufälliger Ausrichtung in Y-Rotation generiert
  * Explosion stoppt an Wänden und Kisten
  * Kisten und Items werden zertört (Player noch nicht)
  * Explosions Dummy um Bombenverhalten zu visualisieren

#### Map Generierung:
  * Basis Map Erzeugung
  * Zufällige Generierung von Türmen und Torböge mit unterschiedlicher Ausrichtung
  * Zufällige Verteilung von Kisten und Items
