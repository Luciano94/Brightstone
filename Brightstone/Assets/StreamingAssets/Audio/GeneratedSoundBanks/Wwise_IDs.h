/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ARROWHIT = 2212241627U;
        static const AkUniqueID BOSSDEATH = 1481017468U;
        static const AkUniqueID BOSSSWORDATTACK = 1787276729U;
        static const AkUniqueID ENEMYARCHERRELEASEBOW = 1108892891U;
        static const AkUniqueID ENEMYARCHERTIGHTBOW = 852912510U;
        static const AkUniqueID ENEMYMELEESWORDATTACK = 2564847700U;
        static const AkUniqueID LEVELCLEAR = 3803289968U;
        static const AkUniqueID LEVELENTER = 122911785U;
        static const AkUniqueID MAPMUSIC = 905671766U;
        static const AkUniqueID MENUITEMCLICK = 3345932315U;
        static const AkUniqueID MENUITEMHOVER = 1220050629U;
        static const AkUniqueID MENUOPEN = 48824776U;
        static const AkUniqueID PLAYERATTACKHEAVY = 3914790867U;
        static const AkUniqueID PLAYERATTACKLIGHT = 231747402U;
        static const AkUniqueID PLAYERATTACKLIGHT2 = 146436428U;
        static const AkUniqueID PLAYERDASH = 2525052962U;
        static const AkUniqueID PLAYERDEATH = 1656947812U;
        static const AkUniqueID PLAYERPARRYHIT = 410952847U;
        static const AkUniqueID PLAYERRESPAWN = 667982098U;
        static const AkUniqueID ROOMBOSSENTER = 4149955699U;
        static const AkUniqueID ROOMCLEAR = 1974576135U;
        static const AkUniqueID ROOMNEWENTER = 1391803776U;
        static const AkUniqueID SELECTATTA = 4283005579U;
        static const AkUniqueID SELECTATTSAME = 1298389756U;
        static const AkUniqueID SELECTATTX = 4283005586U;
        static const AkUniqueID SYNOPSISOPEN = 3349720163U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PLAYERLIFE
        {
            static const AkUniqueID GROUP = 444815956U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID VICTORY = 2716678721U;
            } // namespace STATE
        } // namespace PLAYERLIFE

        namespace STANCE
        {
            static const AkUniqueID GROUP = 1307264345U;

            namespace STATE
            {
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID EXPLORE = 579523862U;
            } // namespace STATE
        } // namespace STANCE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace BOSSHEALTH
        {
            static const AkUniqueID GROUP = 131068444U;

            namespace SWITCH
            {
                static const AkUniqueID BADLYINJURED = 17165122U;
                static const AkUniqueID FLESHWOUND = 720847562U;
                static const AkUniqueID HEALTHY = 2874639328U;
                static const AkUniqueID NEARLYDEAD = 841236080U;
            } // namespace SWITCH
        } // namespace BOSSHEALTH

        namespace PLAYERHEALTH
        {
            static const AkUniqueID GROUP = 151362964U;

            namespace SWITCH
            {
                static const AkUniqueID BADLYINJURED = 17165122U;
                static const AkUniqueID FLESHWOUND = 720847562U;
                static const AkUniqueID HEALTHY = 2874639328U;
                static const AkUniqueID NEARLYDEAD = 841236080U;
            } // namespace SWITCH
        } // namespace PLAYERHEALTH

        namespace PROGRESS
        {
            static const AkUniqueID GROUP = 308635872U;

            namespace SWITCH
            {
                static const AkUniqueID ONETHIRD = 3319853618U;
                static const AkUniqueID THREETHIRDS = 517778677U;
                static const AkUniqueID TWOTHIRDS = 1304826179U;
            } // namespace SWITCH
        } // namespace PROGRESS

        namespace STAGE
        {
            static const AkUniqueID GROUP = 1063701865U;

            namespace SWITCH
            {
                static const AkUniqueID COMMON = 2395677314U;
                static const AkUniqueID FINALE = 2540243936U;
            } // namespace SWITCH
        } // namespace STAGE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID BOSSHP = 1624904154U;
        static const AkUniqueID PLAYERHP = 3657907122U;
        static const AkUniqueID ROOMSCLEARED = 1063810301U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID LEVEL1 = 2678230382U;
        static const AkUniqueID LEVEL1MUSIC = 447814771U;
        static const AkUniqueID MAINMENU = 3604647259U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID BOSS = 1560169506U;
        static const AkUniqueID COMBAT = 2764240573U;
        static const AkUniqueID LVL1 = 141331490U;
        static const AkUniqueID MAINMENU = 3604647259U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID VOICE = 3170124113U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
