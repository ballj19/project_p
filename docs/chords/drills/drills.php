<?php


include_once '/var/www/project_p/docs/drill.php';
include_once '/var/www/project_p/docs/video.php';

$videos = array();

/*********************************************************************************************************************************/

$video = new Video('oqU7-DrpGFY');

$drill = new Drill('Voicing the Major Chords.  Does not work on the 5th Chord.  Go to "Voicing the Dominant Chords" drill for the 5th chord.');
$drill->setVideoTime('1:40', '5:18');
$video->addDrill($drill);

$drill = new Drill('Voicing the Minor Chords');
$drill->setVideoTime('5:19', '7:16');
$video->addDrill($drill);


$drill = new Drill('Voicing the Dominant Chords');
$drill->setVideoTime('7:16', '10:20');
$video->addDrill($drill);

$videos[] = $video;