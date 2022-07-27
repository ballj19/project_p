<?php


include_once '/var/www/project_p/docs/drill.php';
include_once '/var/www/project_p/docs/video.php';

$videos = array();

/*********************************************************************************************************************************/

$video = new Video('bAYBjf8KjSA');

$drill = new Drill('Hold Chord for 1 full bar');
$drill->setVideoTime('2:30', '2:49');
$video->addDrill($drill);

$drill = new Drill('Alternating Rhythm around Beat 4.  Optionally leave out one of the grace notes around beat 4');
$drill->setVideoTime('5:19', '6:04');
$video->addDrill($drill);


$drill = new Drill('Bounce on 8th notes. Optionally add Octave to base and bounce off octave.  Or bounce off 5th.  Switch which hand bounce first');
$drill->setVideoTime('8:31', '9:15');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('JJM8QmoaIyg');

$drill = new Drill('Use inversions');
$drill->setVideoTime('2:05', '3:41');
$video->addDrill($drill);

$drill = new Drill('Add Chord Substitutes');
$drill->setVideoTime('3:41', '5:27');
$video->addDrill($drill);

$drill = new Drill('Use Sus2 Chords');
$drill->setVideoTime('5:27', '8:09');
$video->addDrill($drill);

$drill = new Drill('Increase Rhythmic Frequency');
$drill->setVideoTime('8:09', '10:12');
$video->addDrill($drill);

$drill = new Drill('Add Single Note Driver');
$drill->setVideoTime('10:12', '12:20');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('sflRTCgmpwM');

$drill = new Drill('1st Inversion One Chord Wonder Accent Pattern 1. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drill->setVideoTime('4:20', '6:34');
$video->addDrill($drill);

$drill = new Drill('1st Inversion One Chord Wonder Accent Pattern 2. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drill->setVideoTime('6:50', '8:38');
$video->addDrill($drill);

$drill = new Drill('Upper position One Chord Wonder Accent Pattern 1. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drill->setVideoTime('10:27', '11:01');
$video->addDrill($drill);


$drill = new Drill('Upper position One Chord Wonder Accent Pattern 2. <br> One chord wonder is a 1 sus2 chord in the right hand.');
$drill->setVideoTime('11:01', '11:35');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('d3EWpjiOj8U');

$drill = new Drill('1 Chord Wonder Pop ostinato (Popstinato)');
$drill->setVideoTime('2:22', '4:02');
$video->addDrill($drill);

$drill = new Drill('Add Left Hand');
$drill->setVideoTime('4:02', '6:15');
$video->addDrill($drill);

$drill = new Drill('Invert Pattern');
$drill->setVideoTime('6:53', '6:15');
$video->addDrill($drill);

$drill = new Drill('Open 5ths');
$drill->setVideoTime('6:53', '6:15');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('w3UZHMlWF7U');

$drill = new Drill('Broken Chords');
$drill->setVideoTime('0:56', '3:38');
$video->addDrill($drill);

$drill = new Drill('Classical Pattern');
$drill->setVideoTime('4:40', '6:30');
$video->addDrill($drill);

$drill = new Drill('John Lennon');
$drill->setVideoTime('7:00', '9:26');
$video->addDrill($drill);

$drill = new Drill('Half and Half');
$drill->setVideoTime('9:45', '11:58');
$video->addDrill($drill);

$drill = new Drill('All Rounder');
$drill->setVideoTime('12:25', '14:27');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('rn1ZejMbDR0');

$drill = new Drill('The Humble Power Chord');
$drill->setVideoTime('1:00', '3:08');
$video->addDrill($drill);

$drill = new Drill('The Extended Power Chord');
$drill->setVideoTime('3:28', '5:04');
$video->addDrill($drill);

$drill = new Drill('The Octave 3rd');
$drill->setVideoTime('5:38', '7:45');
$video->addDrill($drill);

$drill = new Drill('The Einaudi Classic');
$drill->setVideoTime('8:27', '10:40');
$video->addDrill($drill);

$drill = new Drill('The Moving Einaudi');
$drill->setVideoTime('11:15', '12:40');
$video->addDrill($drill);

$videos[] = $video;
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