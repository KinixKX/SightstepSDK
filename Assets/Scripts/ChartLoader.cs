using System.Globalization;
using UnityEngine;

public static class ChartLoader
{
    // global data
    private const string tagTitle = "#TITLE:";
    private const string tagArtist = "#ARTIST:";
    private const string tagCredit = "#CREDIT:";
    private const string tagOffset = "#OFFSET:";
    private const string tagInitialBpm = "#BPMS:0.000="; // uh oh

    // per chart
    private const string tagDifficulty = "#DIFFICULTY:";
    private const string tagMeter = "#METER:";
    private const string tagNotes = "#NOTES:";

    // chart parsing stuff
    private const int beatsPerMeasure = 4;
    private const string chartEndDelimiter = ";";
    private const string measureEndDelimiter = ",";

    public static void Load(TextAsset textAsset, SongChart songChart)
    {
        Load(textAsset.text, songChart);
    }

    public static void Load(string file, SongChart songChart)
    {
        bool isSSC = false;

        char[] tapNotes = { '1', '2', '4' };

        object[][][] data = new object[5][][];

        // chartData[4][chartDifficulty][index]
        object[][] chartMetadata = new object[5][];

        for (int i = 0; i < 4; i++)
        {
            // Chart Metadata contains:
            // Index 0: Chart Numeric Difficulty
            chartMetadata[i] = new object[1];
        }

        //  - Global chart data contains
        //  - Index 0: string Song Name
        //  - Index 1: string Song Artist
        //  - Index 2: string Chart Author
        //  - Index 3: float Initial BPM
        //  - Index 4: float Offset
        chartMetadata[4] = new object[5];
        object[] globalChartData = new object[chartMetadata[4].Length];

        // begin parsing
        string[] lines = file.Replace("\r", string.Empty).Split('\n');

        foreach (string line in lines)
        {
            if (line.StartsWith("#NOTEDATA:"))
            {
                isSSC = true;
                break;
            }
        }

        int difficulty = 0; // Beginner, Easy, Medium, Hard, Challenge, Edit
        int chartStartIndex;
        int noteCount;
        object[][] notes;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            // i hate myself for this but it works

            // GLOBAL CHART METADATA
            if (line.StartsWith(tagTitle))
            {
                globalChartData[0] = line.Substring(tagTitle.Length).TrimEnd(';');
            }
            else if (line.StartsWith(tagArtist))
            {
                globalChartData[1] = line.Substring(tagArtist.Length).TrimEnd(';');
            }
            else if (line.StartsWith(tagCredit))
            {
                globalChartData[2] = line.Substring(tagCredit.Length).TrimEnd(';');
            }
            else if (line.StartsWith(tagInitialBpm))
            {
                int firstBpmChange = line.IndexOf(",");
                int endOfBpmList = line.IndexOf(";");

                int indexToUse = firstBpmChange != -1 ? firstBpmChange : endOfBpmList;

                string name = line.Substring(tagInitialBpm.Length, indexToUse - tagInitialBpm.Length);
                float.TryParse(name, NumberStyles.Any, CultureInfo.InvariantCulture, out float bpm);
                globalChartData[3] = bpm;
            }
            else if (line.StartsWith(tagOffset))
            {
                string name = line.Substring(tagOffset.Length).TrimEnd(';');
                float.TryParse(name, NumberStyles.Any, CultureInfo.InvariantCulture, out float offset);
                globalChartData[4] = offset;
            }
            // PER CHART METADATA
            else if (isSSC && line.StartsWith(tagDifficulty)) // select difficulty, sets which chart we're sending data to
            {
                difficulty = GetDifficulty(line.Substring(tagDifficulty.Length).TrimEnd(';'));
            }
            else if (isSSC && line.StartsWith(tagMeter)) // Assign per-difficulty chart metadata for the meter difficulty
            {
                string name = line.Substring(tagMeter.Length).TrimEnd(';');
                int.TryParse(name, out int meter);
                chartMetadata[difficulty][0] = Mathf.Clamp(meter, 1, 10);
            }
            else if (line.StartsWith(tagNotes))
            {
                // this is where our chart starts
                chartStartIndex = i + 1;

                // get the length of the chart
                int len = 0;
                for (int j = chartStartIndex; j < lines.Length; j++)
                {
                    len++;

                    if (lines[j] == chartEndDelimiter)
                        break;
                }

                // create chart array because we know the length now and cannot use lists
                string[] chartLines = new string[len];
                for (int j = 0; j < len; j++)
                {
                    chartLines[j] = lines[j + chartStartIndex];
                }

                if (!isSSC)
                {
                    // DANCE-MODE:
                    // CHART-AUTHOR:
                    // DIFFICULTY: <- we need this
                    // METER: <- we also need this
                    // RADAR-VALUES:
                    string difficultyLine = chartLines[2];
                    string meterLine = chartLines[3];

                    // parse difficulty
                    difficulty = GetDifficulty(difficultyLine.Substring(5).TrimEnd(':'));

                    // parse meter difficulty
                    string meterLineTrimmed = meterLine.Substring(5).TrimEnd(':');
                    int.TryParse(meterLineTrimmed, out int meter);
                    chartMetadata[difficulty][0] = Mathf.Clamp(meter, 1, 10);
                }

                // get the note count so we know how big the object array needs to be
                noteCount = GetNoteCount(chartLines);
                int noteIndex = 0;
                notes = new object[noteCount][];

                double currentBeat = 0;
                int measureLength = 0;
                // initial measure length check!!
                for (int j = isSSC ? 0 : 5; j < chartLines.Length; j++)
                {
                    if (chartLines[j] == measureEndDelimiter || chartLines[j] == chartEndDelimiter)
                        break;
                    measureLength++;
                }

                // Parse the chart data
                for (int j = isSSC ? 0 : 5; j < chartLines.Length; j++)
                {
                    // Current line.
                    string currentLine = chartLines[j].Trim();

                    // Check if it's the end of our chart. ";"
                    if (currentLine == chartEndDelimiter)
                    {
                        break;
                    }
                    // Check if it's the end of a measure. ","
                    else if (currentLine == measureEndDelimiter)
                    {
                        measureLength = 0;
                        // figure out how many lines the upcoming measure has
                        for (int m = j + 1; m < chartLines.Length; m++)
                        {
                            if (chartLines[m] == measureEndDelimiter || chartLines[m] == chartEndDelimiter)
                                break;
                            measureLength++;
                        }
                    }
                    // Check if we have note data. "0000"
                    else if (currentLine.Length == 4) // pretty bad check for a note row
                    {
                        // all notes in row
                        for (int k = 0; k < 4; k++)
                        {
                            // check if its a tap note
                            for (int l = 0; l < tapNotes.Length; l++)
                            {
                                if (currentLine[k] == tapNotes[l])
                                {
                                    // Add a tap note at this beat, on this column
                                    notes[noteIndex] = new object[] { k, currentBeat, false, 0f };
                                    noteIndex++;
                                }
                            }
                        }

                        currentBeat += 1 / (measureLength / (double)beatsPerMeasure);
                    }
                }

                // finalize note data

                switch (difficulty)
                {
                    case 0:
                        data[0] = notes;
                        break;
                    case 1:
                        data[1] = notes;
                        break;
                    case 2:
                        data[2] = notes;
                        break;
                    case 3:
                        data[3] = notes;
                        break;
                    default:
                        break;
                }
            }
        }

        data[4] = chartMetadata;
        data[4][4] = globalChartData;

        SetSongChartData(data, songChart);
    }

    private static void SetSongChartData(object[][][] data, SongChart songChart)
    {
        // finalize
        songChart.simpleChart = data[0];
        songChart.normalChart = data[1];
        songChart.hardChart = data[2];
        songChart.unsightedChart = data[3];

        object[][] chartMetadata = data[4];
        object[] globalChartMetadata = chartMetadata[4];

        // Chart metadata contains:
        // Simple Chart:      [0][x] Length 1
        // Normal Chart:      [1][x] Length 1
        // Hard Chart:        [2][x] Length 1
        // Unsighted Chart:   [3][x] Length 1
        //  - Chart data contains:
        //  - Index 0: int Meter Difficulty
        // Global Chart Data: [4][x]
        //  - Global chart data contains
        //  - Index 0: string Song Name
        //  - Index 1: string Song Artist
        //  - Index 2: string Chart Author
        //  - Index 3: float Initial BPM
        //  - Index 4: float Offset

        for (int i = 0; i < 4; i++) // set per-chart data
        {
            songChart.chartLevel[i] = (int)chartMetadata[i][0];
        }

        songChart.songName = (string)globalChartMetadata[0];
        songChart.songArtist = (string)globalChartMetadata[1];
        songChart.chartsBy = (string)globalChartMetadata[2];
        songChart.bpm = (float)globalChartMetadata[3];
        songChart.songOffset = -(float)globalChartMetadata[4]; // invert the offset

        Debug.Log($"Loaded SM/SSC as {songChart.songName}!");
    }

    private static int GetNoteCount(string[] notes)
    {
        char[] tapNotes = { '1', '2', '4' };
        char[] mines = { 'M' };
        int noteCount = 0;

        foreach (string line in notes)
        {
            if (line.Length == 4) // pretty bad check tbh
            {
                for (int i = 0; i < line.Length; i++)
                {
                    for (int j = 0; j < tapNotes.Length; j++)
                    {
                        if (line[i] == tapNotes[j])
                            noteCount++;
                    }
                }
            }
        }

        return noteCount;
    }

    private static int GetDifficulty(string line)
    {
        switch (line)
        {
            case "Medium":
                return 1;
            case "Hard":
                return 2;
            case "Challenge":
                return 3;
            case "Easy":
            case "Beginner":
            case "Edit":
            default:
                return 0;
        }
    }
}
