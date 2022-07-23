<?php

class Piano {

    public $notes = array();
    public $notes_off = array();
    public $bpm;
    public $time_signature_top;
    public $key;

    function __construct($bpm, $time_signature_top)
    {
        $this->bpm = $bpm;
        $this->time_signature_top = $time_signature_top;

        $allowed_keys = array(0, 2, 4, 5, 7, 9, 11);

        $this->key = rand(0, 11);
        while(!in_array($this->key, $allowed_keys))
        {
            $this->key = rand(0, 11);
        }
    }

    function html()
    {
        $html = "<div class='row'>";
        $html .= '<div class="col-sm-12 align-self-end">';
        $html .= '<h2>Key: ' . $this->keyText() . '</h2>';
        $html .= "</div>";
        $html .= "</div>";
        $html .= "<div class='row justify-content-sm-center'>";
        //$html .= '<div class="col-sm-12 align-self-end">';
        $html .= $this->getProgression();
        //$html .= "</div>";
        $html .= "</div>";
        /*$html .= "<div class='row'>";
        $html .= '<div class="col-sm-12 align-self-end">';
        $html .= "<div class='piano'>";        

        $black_keys = array(22, 25, 27, 30, 32, 34, 37, 39, 42, 44, 46, 49, 51, 54, 56, 58, 61, 63, 66, 68, 70, 73, 75, 78, 80, 82, 85, 87, 90, 92, 94, 97, 99, 102, 104, 106);

        $x_margin = 0;

        for($i = 21; $i <= 108; $i++)
        {
            if(!in_array($i, $black_keys))
            {
                $left = 23 * $x_margin;
                $html .= "<btn style='left: {$left}px;' id='key-{$i}' class='btn btn-light piano-white'></btn>";
                $x_margin++;
            }
            else
            {
                $left = 23 * $x_margin - 7;
                $html .= "<btn style='left: {$left}px;' id='key-{$i}' class='btn btn-dark piano-black'></btn>";
            }
        }

        $html .= "</div>";
        $html .= "</div>";
        $html .= "</div>";*/

        
        //$html .= $this->script();

        return $html;
    }

    function script()
    {
        $biggest_bar = 0;

        foreach($this->notes as $b=>$value)
            if($b > $biggest_bar)
                $biggest_bar = $b;
                
        foreach($this->notes_off as $b=>$value)
            if($b > $biggest_bar)
                $biggest_bar = $b;

        $sleep = 60 * 1000 * $this->time_signature_top / $this->bpm / 16;

        $script = "<script src='/piano/piano.js'></script>";
        $script .= "<script>";
        $script .= "async function loop() {";


        for($bar = 1; $bar <= $biggest_bar; $bar++)
        {
            for($tick = 1; $tick <= 16; $tick++)
            {
                if(array_key_exists($bar, $this->notes) && array_key_exists($tick, $this->notes[$bar]))
                    foreach($this->notes[$bar][$tick] as $note)                    
                        $script .= "activateNote(" . $note . ");";

                if(array_key_exists($bar, $this->notes_off) && array_key_exists($tick, $this->notes_off[$bar]))
                    foreach($this->notes_off[$bar][$tick] as $note)
                        $script .= "deactivateNote(" . $note . ");";
                
                $script .= "await sleep({$sleep});";
            }
        }
        $script .= "loop();";
        $script .= "}";
        $script .= "loop();";

        $script .= "</script>";

        return $script;
    }

    function addNote($bar, $tick, $note, $duration)
    {
        $this->notes[$bar][$tick][] = $note;

        $tick += $duration;

        if($tick > 16)
        {
            $bar++;
            $tick -= 16;
        }    


        $this->notes_off[$bar][$tick][] = $note;
    }

    function addChordNote($bar, $tick, $octave, $chord, $scale_number, $duration)
    {
        $base_note = 36 + $this->key + 12 * $octave;
        $final_note = $base_note;
        $scale_position = 0;

        $major_scale = array(2, 2, 1, 2, 2, 2, 1);

        for($i = 1; $i < $chord + $scale_number - 1; $i++)
        {
            
            $final_note += $major_scale[$scale_position];

            $scale_position++;

            if($scale_position == count($major_scale))
                $scale_position = 0;
        }

        $this->addNote($bar, $tick, $final_note, $duration);
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