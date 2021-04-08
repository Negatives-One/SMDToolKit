# SMD ToolKit

**Equipe Chico Chicken:** João Lucas Oliveira de Sousa, Raimundo Thales Ferreira Ayala Farias, Rebeca Praciano Haddad Carneiro da Cunha, Bruno Carvalho Lima

**Cliente:** Natal Anacleto Chicca Junior


## Descrição

SMD Toolkit é um aplicativo que vai ajudar os estudantes do curso de Sistemas e Mídias Digitais a se organizar melhor para as atividades da faculdade.


## Instrução

O aplicativo irá pedir seu nome de usuário e senha, o nome de usuário pode ser qualquer entrada, mas a senha é **"admin"**.

## Tecnologia 
 
Aqui estão as tecnologias utilizadas neste projeto.
 
* Unity Engine
* C#
* Figma
* Aseprite


## Instalação
 
Na pasta Bin está presente a última build em formato APK para a instalação em dispositivos **Android** e **IOS**.

## Mapeamento de Funcionalidades


|        | Requisito                                | Arquivo                    | Função/Funções                                                                                     | Linhas                                 |
| ------ | ---------------------------------------- | -------------------------- | -------------------------------------------------------------------------------------------------- | -------------------------------------- |
| RF\_F1 | Agendamento de Eventos                   | Agenda.cs                  | SetTaskName, SetDescription, SetPriority, SetCategoria, DeleteTask, UpdateDateTime, DuplicarEvento | 353, 360, 367, 391, 400, 406, 432, 516 |
| RF\_F1 | Agendamento de Eventos                   | GameManager.cs (Singletom) | LoadEvent, LoadEventProperty, UpdateEvent, RemoveEvent, DuplicateEvent                             | 160, 172, 182, 193, 221                |
| RF\_F1 | Agendamento de Eventos                   | AgendarEvento.cs           | SaveEvent                                                                                          | 35                                     |
| RF\_S2 | Calendário                               | Agenda.cs                  | ShowTarefas                                                                                        | 105                                    |
| RF\_F2 | Planejamento de Grade Curricular         | GameManager.cs (Singletom) | LoadDisciplineProperty, LoadDiscipline, UpdateDiscipline                                           | 336, 345, 354                          |
| RF\_F2 | Planejamento de Grade Curricular         | GerenciadorCurso.cs        | OpenMeuCurso, ShowDetails                                                                          | 54, 149                                |
| RF\_S1 | Notificações de Aulas, Trabalhos, Provas | GameManager.cs             | Notify e UpdateNotifications                                                                       | 253 e 279                              |
| RF\_F5 | Customização de Cenário                  | CustomizacaoQuarto.cs      | ShowRoomCustomization e CardClicked                                                                | 42 e 186                               |
| RF\_F5 | Customização de Cenário                  | Quarto.cs                  | UpdateRoom                                                                                         | 32                                     |
