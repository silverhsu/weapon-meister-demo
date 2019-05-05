# weapon-meister-demo
Baseline combat system with minimal attributes but produces weapon variety and counter play

Combat System, 2D
- All Weapons produce Hurtboxes
- All Entities have Hitboxes

Weapons can be force equipped using number keys (1-4)
- Short Sword
- Rapier
- Whip
- Buster Sword

Supports XBOX 360 controller input
- Left control stick movement
- Right control stick attack direction
- Right bumper attack input

Hurtboxes have a minimal attributes
- Damage
- Knockback force
- Knockback type

Weapons use a standard control UnityEvents, allowing for individualized control schema and timing per weapon
- Input Vector2 access
- OnWeaponButtonPressed
- OnWeaponButtonHeld
- OnWeaponButtonReleased
- OnWeaponCooldownComplete
- OnWeaponDelayComplete
- Movement interruption

To Be Implemented
- Instanced controls for up to 4 players
- Environmental traps: Spikes, Sand, and Bumpers
- Enemy Hurtboxes
