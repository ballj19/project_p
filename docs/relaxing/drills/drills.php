<?php


include '/var/www/project_p/docs/drill.php';

$drills = array();

$drill = new Drill();
$drill->addVideo('BduzVqcUMTM', '7:00', '7:40');
$drill->addDescription('He is using 1, 4, 5, and 6 chords. All chords are arpegiated the same way - With the 9th and 10th at the top');
$drills[] = $drill;

$drill = new Drill();
$drill->addVideo('BduzVqcUMTM', '8:40', '11:43');
$drill->addDescription('Choosing Right hand notes.');
$drills[] = $drill;


$drill = new Drill();
$drill->addVideo('BduzVqcUMTM', '11:43', '15:05');
$drill->addDescription('More dramatic arpeggiation');
$drills[] = $drill;


$drill = new Drill();
$drill->addVideo('BduzVqcUMTM', '11:43', '15:05');
$drill->addDescription('Add 6ths to right hand');
$drills[] = $drill;