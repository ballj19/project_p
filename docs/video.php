<?php

include_once '/var/www/project_p/docs/drill.php';

class Video {

    public $id;
    public $title;
    public $description;
    public $drills = array();


    function __construct($id)
    {
        $this->id = $id;
    }

    function setDescription($description)
    {
        $this->setDescription = $description;
    }

    function addDrill($drill)
    {
        $drill->setVideoId($this->id);
        $this->drills[] = $drill;
    }

    function getDrill($d)
    {
        if($d >= count($this->drills))
            return false;

        return $this->drills[$d];
    }
}