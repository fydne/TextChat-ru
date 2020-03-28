# TextChat-ru
[Ответлен от TextChat](https://github.com/iopietro/TextChat)

Плагин TextChat для SCP: SL.

## Требования
[EXILED](https://github.com/galaxy119/EXILED) **1.9.0+**

[LiteDB](https://github.com/mbdavid/LiteDB) **5.0.5+**

## Как установить
Переместите **TextChat.dll** в `%appdata%\Plugins` на **Windows** или в `~/.config/Plugins` на **Linux**.

Переместите **EXILED_Permissions.dll** в `%appdata%\Plugins` на **Windows** или в `~/.config/Plugins` на **Linux**.

Переместите **LiteDB.dll** в `%appdata%\Plugins\dependencies` на **Windows** или в `~/.config/Plugins/dependencies` на **Linux**.

## Как юзать
Нажмите **ё** на клавиатуре, чтобы открыть игровую консоль, после чего вы сможете выполнять команды, чтобы общаться с другими игроками.

### Команды игроков
| Команды | Описание | Аргументы | Примеры |
| --- | --- | --- | --- |
| .chat | Отправляет сообщение чата всем на сервере. | **[Сообщение]** | **.chat всем привет!** |
| .chat_team | Отправляет сообщение в чат вашей команде. | **[Сообщение]** | **.chat_team наконец-то я SCP!** |
| .chat_private | Sends a private chat message to a player. | **[Ник/SteamID64/PlayerID] [Сообщение]** | **.chat_private привет, hmm!** | 
| .help | Отправляет список команд или описание отдельной команды. | **[Название команды]** | **.help/.help chat_team** |


### Команды админов
| Команда | Описание | Аргументы | Права | Примеры |
| --- | --- | --- | --- | --- |
| chat_mute | Замьютить юзера. | **[Ник/SteamID64/PlayerID] [Продолжительность (мин)] [Причина]** | tc.mute | **chat_mute hmm 600 Спам** |
| chat_unmute | Размьютить юзера | **[Ник/SteamID64/PlayerID]** | tc.unmute | **chat_unmute hmm** |

### Конфиги
| название | Тип | По дефолту | Описание |
| --- | --- | --- | --- |
| tc_enabled | True/False | True | Вкл/выкл плагин. |
| tc_database_name | Текст | TextChat | Название базы данных в LiteDB. |
| tc_general_chat_color | Текст | cyan | Цвет глобального чата. |
| tc_private_message_color | Текст | magenta | Цвет личных сообщений. |
| tc_max_message_length | Число | 75 | Максимальная длина сообщения. |
| tc_censor_bad_words | True/False | False | Если включено, то каждое сообщение будет подвергаться цензуре, выбирая слова из списка плохих слов. |
| tc_censor_bad_words_char | Символ | * | Символ, используемый для цензуры сообщений. |
| tc_bad_words | Список | null | Список слов, которые будут подвергаться цензуре в каждом сообщении. |
| tc_save_chat_to_database | True/False | True | Если включено, каждое сообщение, отправленное игроками, будет сохранено в базе данных. |
| tc_can_spectator_send_messages_to_alive | True/False | False | Если включено, зрители смогут отправлять сообщения живым игрокам. |
| tc_show_chat_muted_broadcast | True/False | True | Если включено, то замьюченый юзер узнает о мьюте. |
| tc_chat_muted_broadcast_duration | Число | 10 | Длительность Сообщения |
| tc_chat_muted_broadcast | Строка | <color=red>Вы были отключены от чата на {0} минут, причина: {1}</color> | Сообщение(bc), которое будет показано замьюченому юзеру ({0} и {1} являются местозаполнителями для продолжительности и причины мьюта). |
| tc_show_private_message_notification_broadcast | True/False | True | Если включено, bc будет отправляться игрокам, которые получают личные сообщения. |
| tc_private_message_notification_broadcast_duration | Число | 6 | 	Длительность bc личного сообщения. |
| tc_private_message_notification_broadcast | Строка | Вы получили личное сообщение! | bc, которое будет показано уведомленному игроку. |
| tc_is_slow_mode_enabled | True/False | True | Включить слоумод? |
| tc_slow_mode_interval | Число | 1 | Количество секунд, которое должно пройти, прежде чем игрок сможет отправить другое сообщение. |

### Цвета
| Цвета |
| --- |
| yellow |
| green |
| cyan |
| red |
| magenta |
| black |
| white |
| blue |
| grey |

