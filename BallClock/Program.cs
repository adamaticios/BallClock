using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BallClock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Agility Digital Ball Clock! (Enter 'n' to close)");
            Console.WriteLine("Enter a number between 27 - 127: ");
            bool isStillAlive = true;
            
            while(isStillAlive)
            {
                //Gets input from user
                string input = Console.ReadLine();

                //Checks if user wants to exit the program
                if (input == "n")
                {
                    Environment.Exit(0);
                }
                
                //Verifies the user input is correct.
                if (int.TryParse(input, out int ballCount) && ballCount >= 27 && ballCount <= 127)
                {

                    // Create a bottom queue
                    Queue<int> balls = new Queue<int>();
                    for (int i = 0; i < ballCount; i++)
                        balls.Enqueue(i);
                    // Initialize minute track, 5-minute track and hour track
                    int[] minuteTrack = new int[5];
                    int minuteTrackCount = 0;
                    int[] fiveMinuteTrack = new int[12];
                    int fiveMinuteTrackCount = 0;
                    int[] hourTrack = new int[12];
                    int hourTrackCount = 0;
                    // Start the clock from 00:00
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    int minutesPassed = 0;
                    while (true)
                    {
                        minutesPassed++;
                        // 1 minute passed
                        // Get the first ball in the queue
                        int ball = balls.Dequeue();
                        // Put the ball in the minute track
                        minuteTrack[minuteTrackCount++] = ball;
                        // Check if minute track is full
                        if (minuteTrackCount == 5)
                        {
                            // Put 4 balls in the minute track to the queue in the reverse order
                            for (int i = minuteTrackCount - 2; i >= 0; i--)
                                balls.Enqueue(minuteTrack[i]);
                            minuteTrackCount = 0;
                            // Put the last ball to the 5-minute track
                            fiveMinuteTrack[fiveMinuteTrackCount++] = ball;
                            // Check if 5-minute track is full
                            if (fiveMinuteTrackCount == 12)
                            {
                                // Put 11 balls in the 5-minute track to the queue in the reverse order
                                for (int i = fiveMinuteTrackCount - 2; i >= 0; i--)
                                    balls.Enqueue(fiveMinuteTrack[i]);
                                fiveMinuteTrackCount = 0;
                                // Put the last ball to the hour track
                                hourTrack[hourTrackCount++] = ball;
                                // Check if hour track is full
                                if (hourTrackCount == 12)
                                {
                                    // Put 11 balls in the hour track to the queue in the reverse order
                                    for (int i = hourTrackCount - 2; i >= 0; i--)
                                        balls.Enqueue(hourTrack[i]);
                                    hourTrackCount = 0;
                                    // Put the last ball to the queue
                                    balls.Enqueue(ball);
                                }
                            }
                        }
                        // Check if the balls are in the original order
                        if (minuteTrackCount == 0 && fiveMinuteTrackCount == 0 && hourTrackCount == 0)
                        {
                            bool ordered = true;
                            if (minutesPassed == 24 * 60 * 15)
                            {
                                ordered = true;
                            }
                            int index = 0;
                            foreach (int item in balls)
                            {
                                if (item != index)
                                {
                                    ordered = false;
                                    break;
                                }
                                index++;
                            }
                            // Stop if all the balls are in the original order
                            if (ordered)
                                break;
                        }

                    }
                    // Print the result
                    sw.Stop();
                    Console.WriteLine(ballCount.ToString() + " balls takes " +
                        (minutesPassed / 60 / 24).ToString() + " days, computed in " +
                        sw.ElapsedMilliseconds.ToString() + " ms.");
                    Console.WriteLine("Enter a number between 27 - 127: ");

                }
                else
                {
                    Console.WriteLine("Incorrect input! Please enter a number between 27 - 127: ");
                }
            } 
        }
    }
}
