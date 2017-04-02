# e.DIT

**Edit faster.**

e.DIT helps editors get started editing faster by automatically categorizing footage by scene, letter, and take. It can process gigabytes of film and audio in a few minutes using the Microsoft Cognitive Services API with a custom language model and a neat interpolation technique.

For more information, check out our devpost at https://devpost.com/software/script3r.

## Let's get started!

Here's how to get started categorizing your footage with e.DIT:

1. Download the latest release (------) from the Releases tab of this GitHub repository and extract it to a local folder. 
2. Start eDIT.exe and drag and drop in your footage - don't worry, we take both files and folders!
3. Choose your output directory; if you've run eDIT before, this should fill in automatically. Your footage will be moved to categorized bins within this directory.
4. Press "Label Footage!"

You'll be able to view the progress and transcripts of the files as they process. Once the labeling finishes, Explorer will open automatically to your output directory.

## How does it work?

We use ffmpeg to create a copy of the audio tracks from each of the audio files in monophonic sound at 16kHz. We then send the first 30 seconds of each of these files to a custom language model using Microsoft Cognitive Services that was trained using 740,000 randomly generated slate calls.

Once that's done, we parse the outputted transcript, accounting for both problems that might arise in speech recognition - such as common homophones - and problems that might arise in slating - such as 2nd ACs forgetting scene numbers, letters, takes, or even multiword scene identifiers. So, for instance, we turn "right scene one bread sticks take 3" into "Scene 1B, take 3".

We might not be able to catch all of the information for all of the files, so we then attempt to automatically fill in intermediate files, under the assumption that files with consecutive names were shot consecutively. So, for instance, we can take a list of files where we only know that

- Clip IMG_201 is Scene 1, Take 1
- Clip IMG_204 has a letter of A
- Clip IMG_205 is Take 5
- Clip IMG_206 is from Scene 1

and deduce that

- IMG_201 is Scene 1A Take 1
- IMG_202 is Scene 1A Take 2
- IMG_203 is Scene 1A Take 3
- IMG_204 is Scene 1A Take 4
- IMG_205 is Scene 1A Take 5

We properly handle incomplete information; if we can't conclude for sure that a clip has a given scene, letter, and take, we leave it to the editor on the other end. On a test corpus of the first scene of a short film, this occurred for about 5 out of 14 clips.

Finally, we rename the file and move it to a properly categorized output directory, ready to import into Premiere or your favorite editing software.

## Credits

e.DIT is a project by Vincent Vole, Neil Bickford, Mason Fordham, and Michael Evangelista for LA Hacks 2017! They're pretty proud of it.