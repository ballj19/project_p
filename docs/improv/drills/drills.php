<?php


include_once '/var/www/project_p/docs/drill.php';
include_once '/var/www/project_p/docs/video.php';

$videos = array();

/*********************************************************************************************************************************/

$video = new Video('n_bvzQB_VDI');

$drill = new Drill('Improv Set Up');
$drill->setVideoTime('0:43', '3:58');
$video->addDrill($drill);

$drill = new Drill('Start improvising (Beginner)');
$drill->setVideoTime('7:02', '9:25');
$video->addDrill($drill);

$drill = new Drill('Start improvising (Late Beginner)');
$drill->setVideoTime('10:11', '11:52');
$video->addDrill($drill);

$drill = new Drill('Start improvising (Intermediate)');
$drill->setVideoTime('12:20', '13:29');
$video->addDrill($drill);

$videos[] = $video;

$video = new Video('spljO5VNbbI');

$drill = new Drill('4-on-the-Floor Grip 1.  Explanations of chords at 0:52 in the video');
$drill->setVideoTime('9:20', '11:05');
$video->addDrill($drill);

$drill = new Drill('4-on-the-Floor Grip 2.  Explanations of chords at 0:52 in the video');
$drill->setVideoTime('11:12', '14:08');
$video->addDrill($drill);

$drill = new Drill('4-on-the-Floor Grip 1 Turns.  Explanations of chords at 0:52 in the video');
$drill->setVideoTime('14:10', '16:00');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('BduzVqcUMTM');

$drill = new Drill('He is using 1, 4, 5, and 6 chords. All chords are arpegiated the same way - With the 9th and 10th at the top');
$drill->setVideoTime('7:00', '7:40');
$video->addDrill($drill);

$drill = new Drill('Choosing Right hand notes.');
$drill->setVideoTime('8:40', '11:43');
$video->addDrill($drill);


$drill = new Drill('More dramatic arpeggiation');
$drill->setVideoTime('11:43', '15:05');
$video->addDrill($drill);


$drill = new Drill('Add 6ths to right hand');
$drill->setVideoTime('11:43', '15:05');
$video->addDrill($drill);

$videos[] = $video;


/*********************************************************************************************************************************/

$video = new Video('kNc-chOLmMQ');

$drill = new Drill('Progression and Accompaniment');
$drill->setVideoTime('2:36', '4:43');
$video->addDrill($drill);

$drill = new Drill('Turns');
$drill->setVideoTime('5:53', '8:30');
$video->addDrill($drill);

$drill = new Drill('Slip Notes');
$drill->setVideoTime('9:31', '11:33');
$video->addDrill($drill);

$drill = new Drill('Power Harmony');
$drill->setVideoTime('11:31', '13:28');
$video->addDrill($drill);
$videos[] = $video;