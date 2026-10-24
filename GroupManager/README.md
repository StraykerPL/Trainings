# GroupManager

## How to start project?

- Clone this repo,
- Open "GroupManager.sln" file with Visual Studio, preferably 2022 edition,
- Click on "http" button on VS's toolbar to start app,
- Wait until app has started, browser tab should be opened with application's UI displayed,

## Requirements List

- [x] - User assignment to a group is based on drag & drop principles. By hovering over the user card, holding down the left mouse button, and dragging it to the group component.
- [ ] - Assigning multiple users to a group simultaneously. While holding down the Shift key on the keyboard, it is possible to select multiple unassigned users (slight background change indicating selected users), and then drag these users to the chosen group.
- [ ] - Automatic opening of a specific group tab when hovering over its name with a dragged user.
- [x] - After performing the drag & drop action, the right component should inform the left component about it, which will add a new entry in the list below with the icon of the group to which the user was dragged, along with the First Name and Last Name, and the Group name.
- [ ] - Updating the icon on the user card depending on which group they are in.
- [x] - Dynamic background color change in the user card, alternating every other time for a different color. => done in half,
- [x] - Performing an animation to open the group card (Group 1 is open in the project). The animation should involve smoothly increasing the height of the tab field, then reducing it to the project height, known as Bounce.
- [ ] - Ability to arrange user order in the Group component.
- [ ] - Adding a context menu to each user in the groups - upon right-clicking - with an option to block the user in a specific group, at a specific position.
- [x] - Adding any simple animation when adding a new record to the list of recent actions.
- [x] - The project is adapted to Full HD screens. Prepare the project so that it can also be displayed on smaller screens such as HD or phone screens. Remember to maintain the main principles of Responsive Web Design (RWD). => Basic responsivness,

## Development Principles and Issues

- Skipped all DevOps procedures to speed up work,
- Styles per-component are object-oriented, with BEM syntax class naming,
- Styles are ordered in logical way, component's main elements has styles on top of files, independent elements or additional styles are at the bottom,
- Most of icons are not the same as on mockup, random icons were choosen to not waste time on searching for each icon's name,
- Responsivness is basic, there's no break points and most of responsive scaling is based on flexbox, gridbox, percentages and REMs values, this should be changed,
- There's a bug in User Cards display on both dragging components - cards are taking full height area of free space, causing them to be invisibile stretched on UI,