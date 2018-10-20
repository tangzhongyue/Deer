// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
namespace PitchDetector
{
	public class Detector
	{
		PitchTracker tracker;
		public Detector ()
		{
			tracker = new PitchTracker();
			tracker.PitchRecordHistorySize = 20;
			tracker.RecordPitchRecords = true;
		}

		public void setSampleRate(int samplerate) {
			tracker.SampleRate = samplerate;
		}

		public void DetectPitch(float[] inBuffer) {
			tracker.ProcessBuffer (inBuffer);
		}

		public int findModa(int count) {
			int moda = 0;
			int veces = 0;
			count = (count > tracker.PitchRecordHistorySize) ? tracker.PitchRecordHistorySize : count;
			for (int i=tracker.PitchRecordHistorySize-count; i<tracker.PitchRecordHistorySize; i++) {
				PitchTracker.PitchRecord rec=(PitchTracker.PitchRecord)tracker.PitchRecords[i];
				if(repetitions(i, count)>veces)
					moda=rec.MidiNote;
			}
			return moda;
		}

		public int lastMidiNote (int buffer=0) {
			return tracker.CurrentPitchRecord.MidiNote;
		}

		public float lastMidiNotePrecise (int buffer=0) {
			return (float)tracker.CurrentPitchRecord.MidiNote + ((float)tracker.CurrentPitchRecord.MidiCents/100f);
		}

		public float lastFrequency(int buffer=0) {
			return tracker.CurrentPitchRecord.Pitch;
		}

		public string lastNote(int buffer=0) {
			return PitchDsp.GetNoteName (tracker.CurrentPitchRecord.MidiNote,true,true);
		}

		public string midiNoteToString(int note) {
			return PitchDsp.GetNoteName (note,true,true);
		}

		int repetitions(int element, int count) {
			int rep = 0;
			PitchTracker.PitchRecord refer=(PitchTracker.PitchRecord)tracker.PitchRecords[element];
			int tester=refer.MidiNote;
			for (int i=tracker.PitchRecordHistorySize-count; i<tracker.PitchRecordHistorySize; i++) {
				PitchTracker.PitchRecord rec=(PitchTracker.PitchRecord)tracker.PitchRecords[i];
				if(rec.MidiNote==tester)
					rep++;
			}
			return rep;
		}
	}
}
