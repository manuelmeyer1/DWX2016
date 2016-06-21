# Lab Timeline Tool

![Das Timeline Tool](./images/timelinetool.png)

![Das Timeline Tool](./images/timelinedetailsdrilldown.png)


## Allgemeine Informationen

Das Timeline Tool ist ein Teil vom Diagnostic Hub und als fester Bestandteil in VS2015 integriert. 

Detaillierte Beschreibunen können den folgenden Links entnommen werden:

https://blogs.msdn.microsoft.com/wpf/2015/01/16/new-ui-performance-analysis-tool-for-wpf-applications/
https://msdn.microsoft.com/en-us/library/dn957934.aspx



# Aufgaben

## Lab 1: Grobanalyse des Ladeprozesses

Öffnen Sie die Visual Studio Solution PictureBox.sln. Starten Sie das Projekt. 
Das rotierende Quadrat zeigt an, wie flüssig die Anwendung läuft. Ruckeln oder gar anhalten weist auf eine Überlastung des UI Threads hin. 

Per Knopfdruck auf **Load** werden 5000 Bilder in die ListBox geladen. Wie sie feststellen können führt der Ladeprozess zu einem Einfrieren des gesamten Fensters.

Benützen sie das Timeline Tool über "Debug -> Start Diagnostic Tools Without Debugging" um ein Profiling durchzuführen. Selektieren Sie das Timeline Tool und starten sie das Profiling. 

Analysieren sie die Resultate und finden sie heraus, wo in der Anwendung die Zeit verloren ging. Schauen sie dabei auf Stellen, an welchen die Framerate (FPS) auf Null gesunken ist.

Welche Aktivität hat den UI Thread in die Knie gezwungen?


## Lab 2: Detailanalyse

Markieren sie den auffälligen Teil in der Timeline. Es wird automatisch ein Filter auf den markierten Bereich gesetzt.

Schauen Sie sich im unteren Teil des Fensters die Details an. 

Warum hat der auffällige Block so viel Zeit gekostet?

Welche Aktivität ist dafür verantwortlich?

Welche UIElemente sind betroffen?

Wie viel Zeit wurde mit Disk Zugriff oder Garbage Collection verbraucht?


## Lab 3: Reparatur

Wie kann das Problem gelöst werden? 

Es stellt sich heraus, dass im Beispiel die UI-Virtualisierung, welche die WPF standardmässig unterstützt nicht richtig funktioniert. 

Es gibt gewisse Konfigurationen, welche dafür sorgen, dass UIElemente nicht mehr virtualisieren können. Im vorliegenden Beispiel ist die Ursache eine Konfiguration des Scroll-Modes. Sobald für ein WPF ItemsControl die Property **ScrollViewer.CanContentScroll** auf **FALSE** gesetzt wird, kann die WPF die Höhe der Elemente nicht mehr ermitteln und ist nicht in der Lage, korrekt zu virtualisieren.

Dies führt dazu, dass alle Elemente komplett in die ListBox geladen werden. 

Setzen Sie **ScrollViewer.CanContentScroll** auf **TRUE** und builden sie das Projekt neu.

Führen sie ein erneutes Profiling mit dem Timeline Tool durch und vergleichen sie die Resultate.

