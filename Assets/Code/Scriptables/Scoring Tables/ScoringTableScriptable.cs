using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CreateAssetMenu(fileName = "ScoreTable", menuName = "Scriptables/ScoreTables/ScoreTable")]
    public class ScoringTableScriptable : ScriptableObject
    {
        public ScoreTableLine[] ScoreTableLines;

        public int TranslatePokerScore(bool isHomePlayer, PokerScore pokerScore)
        {
            int basketballScore = -1;

            foreach (ScoreTableLine scoreLine in ScoreTableLines)
            {
                bool scoreFound = false;
                basketballScore = -2;

                if (scoreLine.ScoreType == pokerScore.PokerScoreType)
                {
                    foreach (ScoreLineConditional scoreConditional in scoreLine.ScoreLineConditionals)
                    {
                        basketballScore = -3;

                        if (scoreConditional.ScoreCondition == pokerScore.ScoreValue)
                        {
                            if (isHomePlayer) basketballScore = scoreConditional.HomeScore;
                            else basketballScore = scoreConditional.AwayScore;

                            scoreFound = true;
                            break;
                        }

                        if (scoreFound)
                            break;
                    }

                    if (scoreFound)
                        break;
                }

                if (scoreFound)
                    break;
            }

            ErrorCheck(pokerScore, basketballScore);

            return basketballScore;
        }
        private void ErrorCheck(PokerScore pokerScore, int basketballScore)
        {
            if (basketballScore == 0) Debug.LogWarning("Basketball Score was 0. Likely found condition in table but score was not set " + pokerScore + " with " + pokerScore.PokerScoreType + " and " + pokerScore.ScoreValue);
            else if (basketballScore == -1) Debug.LogWarning("Score Type not found in Score Table for: " + pokerScore + " with " + pokerScore.PokerScoreType + " and " + pokerScore.ScoreValue);
            else if (basketballScore == -2) Debug.LogWarning("Score Type was found, but couldn't find Score Conditional for: " + pokerScore + " with " + pokerScore.PokerScoreType + " and " + pokerScore.ScoreValue);
            else if (basketballScore == -3) Debug.LogWarning("Score Type and Conditional were found, but couldn't find Score Condition Value for: " + pokerScore + " with " + pokerScore.PokerScoreType + " and " + pokerScore.ScoreValue);
            else Debug.Log("Success! Setting score to " + basketballScore);
        }

        public void BuildBaseTable()
        {
            ScoreTableLines = new ScoreTableLine[]
            {
                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.HighCard,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 53,
                            AwayScore = 54,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 54,
                            AwayScore = 55,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 55,
                            AwayScore = 56,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 56,
                            AwayScore = 57,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 57,
                            AwayScore = 58,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 58,
                            AwayScore = 59,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 59,
                            AwayScore = 60,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 60,
                            AwayScore = 61,
                        }
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.PairOne,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 2,
                            HomeScore = 61,
                            AwayScore = 62,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 3,
                            HomeScore = 62,
                            AwayScore = 63,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 4,
                            HomeScore = 63,
                            AwayScore = 64,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 64,
                            AwayScore = 65,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 65,
                            AwayScore = 66,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 66,
                            AwayScore = 67,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 67,
                            AwayScore = 68,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 68,
                            AwayScore = 69,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 69,
                            AwayScore = 70,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 70,
                            AwayScore = 71,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 71,
                            AwayScore = 72,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 72,
                            AwayScore = 73,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 73,
                            AwayScore = 74,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.PairTwo,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 3,
                            HomeScore = 74,
                            AwayScore = 75,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 4,
                            HomeScore = 74,
                            AwayScore = 75,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 75,
                            AwayScore = 76,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 75,
                            AwayScore = 76,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 76,
                            AwayScore = 77,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 76,
                            AwayScore = 77,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 77,
                            AwayScore = 78,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 77,
                            AwayScore = 78,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 78,
                            AwayScore = 79,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 79,
                            AwayScore = 80,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 80,
                            AwayScore = 81,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 81,
                            AwayScore = 82,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.Trips,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 2,
                            HomeScore = 82,
                            AwayScore = 83,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 3,
                            HomeScore = 83,
                            AwayScore = 84,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 4,
                            HomeScore = 84,
                            AwayScore = 85,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 85,
                            AwayScore = 86,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 86,
                            AwayScore = 87,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 87,
                            AwayScore = 88,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 88,
                            AwayScore = 89,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 89,
                            AwayScore = 90,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 90,
                            AwayScore = 91,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 91,
                            AwayScore = 92,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 92,
                            AwayScore = 93,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 93,
                            AwayScore = 94,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 94,
                            AwayScore = 95,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.Straight,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 95,
                            AwayScore = 96,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 95,
                            AwayScore = 96,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 95,
                            AwayScore = 96,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 96,
                            AwayScore = 97,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 96,
                            AwayScore = 97,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 96,
                            AwayScore = 97,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 97,
                            AwayScore = 98,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 97,
                            AwayScore = 98,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 97,
                            AwayScore = 98,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 98,
                            AwayScore = 99,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.Flush,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 99,
                            AwayScore = 100,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 99,
                            AwayScore = 100,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 99,
                            AwayScore = 100,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 100,
                            AwayScore = 101,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 100,
                            AwayScore = 101,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 101,
                            AwayScore = 101,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 101,
                            AwayScore = 102,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 102,
                            AwayScore = 103,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 103,
                            AwayScore = 104,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.FullHouse,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 2,
                            HomeScore = 104,
                            AwayScore = 105,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 3,
                            HomeScore = 104,
                            AwayScore = 105,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 4,
                            HomeScore = 104,
                            AwayScore = 105,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 104,
                            AwayScore = 105,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 105,
                            AwayScore = 106,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 105,
                            AwayScore = 106,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 105,
                            AwayScore = 106,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 105,
                            AwayScore = 106,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 106,
                            AwayScore = 107,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 106,
                            AwayScore = 107,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 106,
                            AwayScore = 107,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 106,
                            AwayScore = 107,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 107,
                            AwayScore = 108,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.Fours,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 2,
                            HomeScore = 109,
                            AwayScore = 110,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 3,
                            HomeScore = 108,
                            AwayScore = 109,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 4,
                            HomeScore = 108,
                            AwayScore = 109,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 108,
                            AwayScore = 109,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 109,
                            AwayScore = 110,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 109,
                            AwayScore = 110,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 109,
                            AwayScore = 110,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 109,
                            AwayScore = 110,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 110,
                            AwayScore = 111,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 110,
                            AwayScore = 111,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 110,
                            AwayScore = 111,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 111,
                            AwayScore = 112,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 112,
                            AwayScore = 113,
                        },
                    }
                },

                new ScoreTableLine
                {
                    ScoreType = Enums.PokerScoreType.StraightFlush,
                    ScoreLineConditionals = new ScoreLineConditional[]
                    {
                        new ScoreLineConditional
                        {
                            ScoreCondition = 5,
                            HomeScore = 114,
                            AwayScore = 115,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 6,
                            HomeScore = 114,
                            AwayScore = 115,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 7,
                            HomeScore = 114,
                            AwayScore = 115,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 8,
                            HomeScore = 114,
                            AwayScore = 115,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 9,
                            HomeScore = 115,
                            AwayScore = 116,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 10,
                            HomeScore = 115,
                            AwayScore = 116
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 11,
                            HomeScore = 116,
                            AwayScore = 117,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 12,
                            HomeScore = 116,
                            AwayScore = 117,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 13,
                            HomeScore = 116,
                            AwayScore = 117,
                        },

                        new ScoreLineConditional
                        {
                            ScoreCondition = 14,
                            HomeScore = 117,
                            AwayScore = 118,
                        },
                    }
                }
            };
        } 
    }


    [System.Serializable]
    public class ScoreTableLine
    {
        public Enums.PokerScoreType ScoreType;
        public ScoreLineConditional[] ScoreLineConditionals;
    }

    [System.Serializable]
    public class ScoreLineConditional
    {
        [Range(0, 14)] public int ScoreCondition;
        public int HomeScore;
        public int AwayScore;
    }
}
