<?php 

function sectionHtml($title, $path)
{
    $html = '<h2 style="padding-top: 50px;">' . $title . '</h2>';

    $html .=    '<div class="row gx-4 gx-lg-5 h-100 text-center">';
    
    include_once "{$path}/drills/drills.php";
    
    $v = 0;
    foreach($videos as $video)
    {
        
        $html .=            "<div class='col-sm-auto'>";
        $html .=                "<a href={$path}/drills?v={$v}&d=0><img style='margin-top: 50px' src='https://img.youtube.com/vi/{$video->id}/0.jpg'></a>";
        $html .=            "</div>";
    
        $v++;
    }
    
    $html .=    '</div>';

    return $html;
}

?>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Piano Drills</title>
        <!-- Favicon-->
        <link rel="icon" type="image/x-icon" href="assets/favicon.ico" />
        <!-- Bootstrap Icons-->
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css" rel="stylesheet" />
        <!-- Google fonts-->
        <link href="https://fonts.googleapis.com/css?family=Merriweather+Sans:400,700" rel="stylesheet" />
        <link href="https://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic" rel="stylesheet" type="text/css" />
        <!-- SimpleLightbox plugin CSS-->
        <link href="https://cdnjs.cloudflare.com/ajax/libs/SimpleLightbox/2.1.0/simpleLightbox.min.css" rel="stylesheet" />
        <!-- Core theme CSS (includes Bootstrap)-->
        <link href="css/styles.css" rel="stylesheet" />
    </head>
    <body class="bg-dark text-white" style="">

<?php

$html = '<div class="container px-4 px-lg-5" style="margin-bottom: 50px">';

$html .= sectionHtml('Chords', 'chords');
$html .= sectionHtml('Accompaniment', 'accompaniment');
$html .= sectionHtml('Improv', 'improv');

$html .= '</div>';

echo $html;