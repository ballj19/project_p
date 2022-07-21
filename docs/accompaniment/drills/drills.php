<?php


include '/var/www/project_p/docs/drill.php';

$drills = array();

$drill = new Drill('Hold Chord for 1 full bar');
$drill->addVideo('bAYBjf8KjSA', '2:30', '2:49');
$drills[] = $drill;

$drill = new Drill('Alternating Rhythm around Beat 4');
$drill->addVideo('bAYBjf8KjSA', '5:19', '6:04');
$drill->addOption('Leave out one of the grace notes around beat 4');
$drills[] = $drill;

$drill = new Drill('Bounce 8th Notes');
$drill->addVideo('bAYBjf8KjSA', '5:19', '6:04');
$drill->addOption('Add Octave to Count 1.  Bounce on higher octave note.');
$drill->addOption('Add 5th to Count 1.  Bounce on 5th.');
$drill->addOption('Swap which hand bounces first.');
$drills[] = $drill;
