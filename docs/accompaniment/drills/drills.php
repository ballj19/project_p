<?php


include '/var/www/project_p/docs/drill.php';

$drills = array();


$drill = new Drill();
$drill->addVideo('bAYBjf8KjSA', '2:30', '2:49');
$drill->addDescription('Hold Chord for 1 full bar');
$drills[] = $drill;

$drill = new Drill('Alternating Rhythm around Beat 4');
$drill->addVideo('bAYBjf8KjSA', '5:19', '6:04');
$drill->addDescription('Alternating Rhythm around Beat 4.  Optionally leave out one of the grace notes around beat 4');
$drills[] = $drill;


$drill = new Drill();
$drill->addVideo('bAYBjf8KjSA', '8:31', '9:15');
$drill->addDescription('Bounce on 8th notes. Optionally add Octave to base and bounce off octave.  Or bounce off 5th.  Switch which hand bounce first');
$drills[] = $drill;


$drill = new Drill();
$drill->addVideo('JJM8QmoaIyg', '2:05', '3:41');
$drill->addDescription('Use inversions');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('JJM8QmoaIyg', '3:41', '5:27');
$drill->addDescription('Add Chord Substitutes');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('JJM8QmoaIyg', '5:27', '8:09');
$drill->addDescription('Use Sus2 Chords');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('JJM8QmoaIyg', '8:09', '10:12');
$drill->addDescription('Increase Rhythmic Frequency');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('JJM8QmoaIyg', '10:12', '12:20');
$drill->addDescription('Add Single Note Driver');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('sflRTCgmpwM', '4:20', '6:34');
$drill->addDescription('1st Inversion One Chord Wonder Accent Pattern 1. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('sflRTCgmpwM', '6:50', '8:38');
$drill->addDescription('1st Inversion One Chord Wonder Accent Pattern 2. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('sflRTCgmpwM', '10:27', '11:01');
$drill->addDescription('Upper position One Chord Wonder Accent Pattern 1. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drills[] = $drill;


$drill = new Drill();
$drill->addVideo('sflRTCgmpwM', '11:01', '11:35');
$drill->addDescription('Upper position One Chord Wonder Accent Pattern 2. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drills[] = $drill;

/*
$piano = new Piano(60, 4);
//bar, tick, octave, chord, number, duration
$piano->addChordNote(1, 1, 0, 1, 1, 1);
$piano->addChordNote(1, 1, 1, 1, 1, 2);
$piano->addChordNote(1, 1, 1, 1, 3, 2);
$piano->addChordNote(1, 1, 1, 1, 5, 2);

$piano->addChordNote(1, 3, 0, 1, 1, 2);

$piano->addChordNote(1, 5, 1, 1, 1, 2);
$piano->addChordNote(1, 5, 1, 1, 3, 2);
$piano->addChordNote(1, 5, 1, 1, 5, 2);


$piano->addChordNote(1, 7, 0, 1, 1, 2);

$piano->addChordNote(1, 9, 1, 1, 1, 2);
$piano->addChordNote(1, 9, 1, 1, 3, 2);
$piano->addChordNote(1, 9, 1, 1, 5, 2);

$piano->addChordNote(1, 11, 0, 1, 1, 2);

$piano->addChordNote(1, 13, 1, 1, 1, 2);
$piano->addChordNote(1, 13, 1, 1, 3, 2);
$piano->addChordNote(1, 13, 1, 1, 5, 2);

$piano->addChordNote(1, 15, 0, 1, 1, 2);
$drill->addPiano($piano);
*/