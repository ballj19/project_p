<?php


include_once '/var/www/project_p/docs/drill.php';
include_once '/var/www/project_p/docs/video.php';

$videos = array();


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

/*********************************************************************************************************************************/

$video = new Video('5U6fFx8F15M');

$drill = new Drill('Standard Pattern');
$drill->setVideoTime('1:52', '3:00');
$video->addDrill($drill);

$drill = new Drill('Add Melody');
$drill->setVideoTime('3:35', '4:33');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('9D63K8CPgBg');

$drill = new Drill('Pattern');
$drill->setVideoTime('0:39', '1:40');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('1-isTjm5JNE');

$drill = new Drill('Section 1');
$drill->setVideoTime('6:08', '6:25');
$video->addDrill($drill);

$drill = new Drill('Section 2');
$drill->setVideoTime('6:52', '6:57');
$video->addDrill($drill);

$drill = new Drill('Section 3');
$drill->setVideoTime('7:47', '7:56');
$video->addDrill($drill);

$drill = new Drill('Section 4');
$drill->setVideoTime('8:50', '8:56');
$video->addDrill($drill);

$drill = new Drill('Add Left Hand.  Watch next video to flesh out left hand.');
$drill->setVideoTime('11:18', '11:55');
$video->addDrill($drill);

$videos[] = $video;

/*********************************************************************************************************************************/

$video = new Video('PCXgnvpEujE');

$drill = new Drill('Left Hand Power Chord');
$drill->setVideoTime('5:40', '5:58');
$video->addDrill($drill);

$drill = new Drill('Broken 5th Octaves');
$drill->setVideoTime('8:00', '8:16');
$video->addDrill($drill);

$drill = new Drill('10th Chords.  He is using the 9th chord on the C Minor as a personal flare.');
$drill->setVideoTime('12:23', '12:42');
$video->addDrill($drill);

$drill = new Drill('Double up 10th note between beat 3 and 4');
$drill->setVideoTime('13:30', '14:10');
$video->addDrill($drill);

$videos[] = $video;


/*********************************************************************************************************************************/

$video = new Video('FoNSxSSwDRA');

$drill = new Drill('Pattern');
$drill->setVideoTime('1:38', '2:20');
$video->addDrill($drill);

$videos[] = $video;
