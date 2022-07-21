<?php

class Drill {
    public $description;
    public $options = '';
    public $videos = array();
    public $notes = array();

    function __construct($description = '')
    {
        $this->description = $description;
    }

    function html()
    {
        
        $html = '<header class="masthead bg-dark text-white"><div class="container px-4 px-lg-5 h-100">';
        


        $html .= '<div class="row gx-4 gx-lg-5 h-100 align-items-center justify-content-center text-center">';
        $html .= "<div class='row'>";
        $html .= '<div class="col-sm-12 align-self-end">';
        $html .= "<h3>{$this->description}</h3>";   
        $html .= "<hr class='divider'>";
        $html .= '</div>';

        $html .= '<div class="col-sm-12 align-self-end">';
        $html .= '<a class="btn btn-secondary btn-xl" href="#">Randomize Progression</a>';
        $html .= "</div>";
        $html .= "</div>";
        $html .= "<div class='row'>";
        $html .= "<div class='col-sm-6'>";     
        if(strlen($this->options) > 0)
            $html .= "<div style='margin-top: 20px; text-align: left'><b>Options</b><ul>{$this->options}</ul></div>";
        $html .= "</div>";

        $html .= "<div class='col-sm-6'>";
        foreach($this->videos as $video)
            $html .= "<iframe width='560' height='315' src='{$video}' title='YouTube video player' frameborder='0' allowfullscreen></iframe>";
        $html .= "</div>";
        $html .= "</div>";


        $html .=  '</div>
            </div>';

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

    function addOption($option)
    {
        $this->options .= "<li>{$option}</li>";
    }
}