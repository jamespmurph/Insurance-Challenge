S# Completion Notes

## How I approached the task

I Started by reviewing the existing code to understand the patterns that would
help me before adding any new code, and so the code i did add would stay consistent
with the existing code. From there i worked through the three main gaps, implementing
Save inMemoryClaimRespository, filling in the real logic for ClaimHandler.CreateClaim
and completing the console input flow in program.cs, following CreatePolicy as an template.
Once all that was working end to end i extended the code based on feedback i got from testing.
First, i updated policy creation to actually show the new policys ID, second i addedm retry
loops to the console input flow so that if a user enters invalid data they can try again instead 
of crashing the application. And lastly i added new ways to view data such as view all policies, 
view all claims and view all claims for a specific policy. By first adding the methods to the 
repositories, then writing up new menu options in program.cs to use them.

## Decisions I made

- I used ArgumentException for missing or invalid input and ArgumentOutOfRangeException
for values outside a valid range, each with a specific message, rather then one generic 
exception for all invalid input.

- Policy existence is checked first since every other check depends on having
a valid policy to compare against.

- amountClaiming > 0 not >=0 since the spec said "greater then zero" so i treated 
0 as invalid too.

- CreatePolicy returns the new Policy ID to the console as there was no way of testing
claims without it, returning the id from the handler and printing it out in program.cs
keeps console.writeline calls out of the buisness logic.

- Kept the "view" lists read only, made sure the data handed back can be looked at but not
accidentally modified by the caller.

- Retry loops for format validation, instead of taking user all the way back to the main
menu, each field loops until it gets valid input, so one typo doesn't force the user to start over.

## What I would do differently or extend given more time

- Stop storing claims in two places, currently when a claim is created it gets saved to both the
repository and the claims list, which could drift out of sync. I would pick one and drop the other.

- Persistence beyond in memory, the app currently loses all data on exit, a real project would need 
a database or file storage to persist data between runs.

- Better formatting of the console output, the current output is functinoal but not very user friendly,
i would look at some column alignment or paging to help readability.

## AI usage

If you used any AI tooling during this exercise, log it honestly below.

| Prompt | Output summary |
|--------|----------------|
"im getting a CS0136 error on StartDate at a specific line in program.cs attached is the specific code      |  It identified that there was a duplicate declaration of StartDate within overlapping
block. Explain what is causing the naming conflict without re writing any of my whole input handling flow,  |  scopes.
since i only want to fix this one variable scope issue".

"can you read through this project. I want to understand its current setup and how data currently flows     |  It identified that program.cs was the main menu file that called off to methods in
so that i can extend it and add more functionality with the same structure"                                 |  the handler files depending on the number selected in the menu. The handler files contain
                                                                                                               the appropiate and relevant logic and use the in memory repositories to save the data. The 
                                                                                                               interfaces were used to acsess the methodsw in the in memory repositorys.