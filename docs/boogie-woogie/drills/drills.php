<?php


include_once '/var/www/project_p/docs/drill.php';
include_once '/var/www/project_p/docs/video.php';

$videos = array();

/*********************************************************************************************************************************/

$video = new Video('kuDRaoy8RlE');

$drill = new Drill('Left Hand');
$drill->setVideoTime('3:00', '5:18');
$video->addDrill($drill);

$drill = new Drill('Add Right Hand');
$drill->setVideoTime('4:02', '4:15');
$video->addDrill($drill);

$drill = new Drill('Pull Back');
$drill->setVideoTime('5:30', '6:00');
$video->addDrill($drill);

$drill = new Drill('Different Riff');
$drill->setVideoTime('9:08', '9:30');
$video->addDrill($drill);

$videos[] = $video;