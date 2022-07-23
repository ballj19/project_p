<?php

include_once 'piano.php';

class Drill {
    public $description;
    public $videos = array();
    public $notes = array();
    public $piano;
    public $key;

    function __construct()
    {        
        $allowed_keys = array(0, 2, 4, 5, 7, 9, 11);

        $this->key = rand(0, 11);
        while(!in_array($this->key, $allowed_keys))
        {
            $this->key = rand(0, 11);
        }
    }

    function html()
    {
        $drill_number = isset($_GET['n']) ? $_GET['n'] : 0;
        $prev = $drill_number > 0 ? $drill_number - 1 : 0;
        $next = $drill_number + 1;

        
        $html = '<header class="masthead bg-dark text-white"><div class="container px-4 px-lg-5 h-100">';
        $html .=    '<div class="row gx-4 gx-lg-5 h-100 align-items-center justify-content-center text-center">';
        $html .=        "<div class='row justify-content-sm-center'>";
        $html .=            '<div class="col-sm-auto align-self-end">';
        $html .=                "<a class='btn btn-light' href='?n={$prev}'>Prev</a>";
        $html .=            '</div>';
        $html .=            '<div class="col-sm-auto align-self-end">';
        $html .=                "<a class='btn btn-light' href='?n={$next}'>Next</a>";
        $html .=            '</div>';
        $html .=        "</div>";
        $html .=        "<div class='row'>";
        $html .=            "<div class='col-sm-12'>";
        foreach($this->videos as $video)
            $html .=            "<iframe width='1200' height='675' src='{$video}' title='YouTube video player' frameborder='0' allowfullscreen></iframe>";
        $html .=            "</div>";
        $html .=        "</div>";
        $html .=        "<div class='row'>";
        $html .=            '<div class="col-sm-12 align-self-end">';
        $html .=                "<h3>{$this->description}</h3>";   
        $html .=            '</div>';
        $html .=        "</div>";
        $html .=        "<div class='row'>";
        $html .=            '<div class="col-sm-12 align-self-end">';
        $html .=               '<h2>Key: ' . $this->keyText() . '</h2>';
        $html .=            "</div>";
        $html .=         "</div>";
        $html .=        "<div class='row justify-content-sm-center'>";
        $html .=            $this->getProgression();
        $html .=        "</div>";
        $html .=    '</div>';
        $html .= '</div>';
        $html .= '</header>';

        return $html;
    }

    function addVideo($id, $start, $end)
    {
        $start_minutes = explode(':', $start)[0];
        $start_seconds = explode(':', $start)[1] + 60 * $start_minutes;

        $end_minutes = explode(':', $end)[0];
        $end_seconds = explode(':', $end)[1] + 60 * $end_minutes;


        $video = "https://www.youtube.com/embed/{$id}?start={$start_seconds}&end={$end_seconds}";

        $this->videos[] = $video;
    }

    function addDescription($description)
    {
        $this->description = $description;
    }

    function addPiano($piano)
    {
        $this->piano = $piano;
    }
    

    function keyText()
    {
        $key_text = array("C", "C#", "D", "Eb", "E", "F", "F#", "G", "Ab", "A", "Bb", "B");

        return $key_text[$this->key];
    }

    function getProgression()
    {
        $progressions = array();

        $progressions[] = array('I', 'IV', 'V');
        $progressions[] = array('I', 'V', 'iv', 'IV');
        $progressions[] = array('ii', 'V', 'I');
        $progressions[] = array('vi', 'IV', 'I', 'V');
        $progressions[] = array('I', 'IV', 'vi', 'V');

        $p = rand(0, count($progressions) - 1);

        $html = "";

        foreach($progressions[$p] as $chord)
        {
            $html .= "<div class='col-sm-auto'><h2>" . $chord . "</h2></div>";
        }


        return $html;
    }
}