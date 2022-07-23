<?php


include '/var/www/project_p/docs/drill.php';

$drills = array();


$drill = new Drill();
$drill->addVideo('n_bvzQB_VDI', '0:43', '3:58');
$drill->addDescription('Improv Set Up');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('n_bvzQB_VDI', '7:02', '9:25');
$drill->addDescription('Start improvising (Begginer)');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('n_bvzQB_VDI', '10:11', '11:52');
$drill->addDescription('Start improvising (Late Begginer)');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('n_bvzQB_VDI', '12:20', '13:29');
$drill->addDescription('Start improvising (Intermediate)');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('spljO5VNbbI', '9:20', '11:05');
$drill->addDescription('4-on-the-Floor Grip 1.  Explanations of chords at 0:52 in the video');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('spljO5VNbbI', '11:12', '14:08');
$drill->addDescription('4-on-the-Floor Grip 2.  Explanations of chords at 0:52 in the video');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('spljO5VNbbI', '14:10', '16:00');
$drill->addDescription('4-on-the-Floor Grip 1 Turns.  Explanations of chords at 0:52 in the video');
$drills[] = $drill;