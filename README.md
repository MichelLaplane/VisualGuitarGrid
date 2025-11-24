# VisualGuitarGrid

Windows Forms C# application to create guitar grids (grilles d'accords) inspired by https://back2guitar.com/grille-accord-guitare/.

How to open
- Requires Visual Studio 2026 or later with .NET desktop development workload.
- Open the solution `VisualGuitarGrid.sln`.
- Build and run.

Usage
- Set number of strings and frets, edit tuning (space or comma separated), then click Update.
- Click on a fret intersection to place a note (left click). Use the finger selector to add a finger number.
- Shift+Click to set an open string. Right-click to toggle muted (X).
- Enter an optional chord name and export as PNG.

This is a first version. Enhancements possible: barre chords, presets, SVG export, printing.

Short story of what I did and why I implemented a small Windows Forms (.NET 8) application to draw and edit guitar chord grids (grilles d'accords) based on the Back2Guitar layout you referenced. The app supports configuring number of strings and frets, entering tuning, placing fretted notes, marking open (Shift+Click) and muted strings (right-click), adding a finger number, entering a chord name and exporting the diagram as a PNG. I prepared a Visual Studio solution and performed the initial commit to your repo’s main branch.

How to run the project locally

Clone the repository: git clone https://github.com/MichelLaplane/VisualGuitarGrid.git
Open VisualGuitarGrid.sln in Visual Studio (you requested VS 2026 — the project targets net8.0-windows).
Build and run the VisualGuitarGrid project.
