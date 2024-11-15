Game Data

--------------------------------------------------
01_Player

- 체력, 포만감, 갈증, 체온 등의 PlayerData 갖고
- 걷기, 채집하기, 식사하기, 건축, 사냥하기

- 체력 : 포만감 0, 갈증 0, 체온 일정 수치 이하 시 감소
- 포만감 : 지속적으로 감소
- 갈증 : 지속적으로 감소
- 체온 : 특정 위치에 따라 변경

- 식사 : 포만감 회복
- 코코넛 마시기 : 갈증 회복
- 불 : 가까이 가면 체온 상승

- 사망 시 : 유령상태로 변경, 모델x 상호작용x

- GameClear : 하루{10분) 생존
- GameOver : 모든 Player의 HP 가 0 이하로 감소
--------------------------------------------------
02_Tree

- ‘Tree’ 는 필드에 한정적으로 배치되어 있으며, ‘Stone’ 와 일정 횟수 이상 충돌하면 ‘Tree’ 는 ‘Log’ 로 대체된다.
- ‘Log’ 는 Player가 Grap할 수 있으며, ‘Stone’ 와 일정 횟수 이상 충돌하면 ‘Log’ 는 ‘Panel’ 로 대체된다.
- ‘Panel’ 는  Player가 Grap할 수 있으며, ‘Bamboo’ 와 충돌한 상태에서 ‘Stone’ 와 일정 횟수 이상 충돌하면 ‘Panel’ 오브젝트의 IsKinetic 은 true가 된다.

- ‘Fire’ 와 충돌
- ‘Animal’ 와 충돌
--------------------------------------------------
03_ Stone

- ‘Stone’ 는 Player가 Grap할 수 있으며, Grpa한 상태로 다른 Object와 충돌하면 해당 Object에게 Damage 입힌다.

 Basic Version
- 'Stone' 은 'Stone'과 부딪히면 일정 반경내의 'FireWood'를 탐색 -> 'FireWood'에 'Fire'를 생성한다.

  Extra Version
- ‘Stone’ 는 Player가 Grap한 상태로 ‘Stone’ 와 부딪히면, 2개의 Object가 소멸되고 ‘CutStone’ 가 Player 의 앞에 생성된다.
- ‘CutStone’ 은 Player가 Grap할 수 있으며, Grap한 상태로 ‘Stick’ 에 충돌하면 2개의 Object가 소멸되고 ‘Weapon’이 Player 의 앞에 생성된다.
- 'Stone' 은 'Flint'과 부딪히면 일정 반경내의 'FireWood'를 탐색 -> 'FireWood'에 'Fire'를 생성한다.
--------------------------------------------------
