# Design Doc: Vampire Survival (Vampire Survivors-like)

## 1. Mục tiêu & tầm nhìn
- **Tầm nhìn**: Game survival top-down đơn giản, nhịp nhanh, tập trung vào “power fantasy” tăng sức mạnh liên tục.
- **Mục tiêu chính**: 10–20 phút chơi / run, cảm giác “one more run”, dễ vào, khó master.
- **Đối tượng**: Người chơi casual và mid-core thích hành động, xây build.

## 2. Trụ cột thiết kế (Design Pillars)
- **Sống sót trước đàn quái**: đông, áp lực tăng dần, không có nút tấn công thủ công.
- **Build đa dạng**: kết hợp vũ khí + nâng cấp để tạo synergy.
- **Tiến triển rõ rệt**: tăng cấp liên tục, game trở nên “đã tay”.
- **Nhịp nhanh & rõ ràng**: UI gọn, hiệu ứng đọc được, không rối.

## 3. Phạm vi & nền tảng
- **Nền tảng**: PC (Windows), tối ưu cho bàn phím/mouse.
- **Camera**: Top-down 2D.
- **Thời lượng**: 1 run khoảng 10–20 phút.

## 4. Vòng lặp gameplay (Core Loop)
1. Di chuyển né đòn → thu thập exp/tiền.
2. Lên cấp → chọn 1 nâng cấp.
3. Sức mạnh tăng → sống sót lâu hơn.
4. Kết thúc run → nhận phần thưởng meta.
5. Dùng meta để mở khóa → vào run tiếp.

## 5. Điều khiển
- **WASD**: di chuyển.
- **Esc**: pause.
- **Chuột**: điều hướng camera (nếu cần) / chọn UI.

## 6. Hệ thống nhân vật
### 6.1 Chỉ số cơ bản
- **HP**: máu nhân vật.
- **Speed**: tốc độ di chuyển.
- **Damage**: sát thương tổng.
- **Cooldown**: giảm thời gian hồi chiêu vũ khí.
- **Area**: bán kính/diện tích hiệu ứng.
- **Pickup Range**: phạm vi hút exp/loot.

### 6.2 Tăng cấp
- Mỗi cấp: chọn 1 nâng cấp từ 3–4 lựa chọn.
- Có **rarity** cho nâng cấp (common/rare/epic).

## 7. Vũ khí & nâng cấp
### 7.1 Vũ khí mẫu
- **Magic Wand**: bắn tự động gần mục tiêu.
- **Whip**: quét hình vòng cung phía trước.
- **Garlic Aura**: sát thương AoE xung quanh.
- **Throwing Axe**: ném parabol xuyên quái.
- **Holy Book**: vòng quay quanh nhân vật.

### 7.2 Nâng cấp & synergy
- Mỗi vũ khí có 5–7 cấp.
- Khi đạt điều kiện có thể “evolve” (VD: Magic Wand + Spell Tome).
- **Synergy**: tăng damage khi kết hợp 2–3 vũ khí hỗ trợ nhau.

## 8. Kẻ địch
### 8.1 Loại quái
- **Melee chậm**: đông, dễ giết.
- **Melee nhanh**: ít hơn, áp lực cao.
- **Ranged**: bắn xa, buộc né.
- **Elite**: máu nhiều, rơi thưởng lớn.

### 8.2 Boss
- Xuất hiện ở phút 10 và 20.
- Có pattern rõ ràng, drop vật phẩm tiến hóa.

## 9. Level & môi trường
- **Map**: vô hạn, sinh terrain ngẫu nhiên nhẹ (cây, đá).
- **Vật cản**: giảm, tránh gây kẹt.
- **Pickups**: exp, heal, gold, chest.

## 10. UI/UX
- **HUD**: HP, thời gian sống, cấp độ, exp bar.
- **Góc nâng cấp**: hiển thị mô tả rõ, icon lớn.
- **Pause**: resume, settings, quit run.

## 11. Âm thanh & hiệu ứng
- **SFX**: hit, pickup, level-up, boss warning.
- **BGM**: 1–2 bản loop theo giai đoạn.
- **VFX**: rõ ràng, không che nhân vật.

## 12. Meta progression
- **Currency**: gold từ run.
- **Unlocks**: mở nhân vật, vũ khí, nâng cấp start bonus.
- **Perks**: +HP start, +Speed, +Pickup Range.

## 13. Kiến trúc kỹ thuật (Unity)
### 13.1 Scene
- **MainMenu**: start game, options.
- **GamePlay**: toàn bộ run.
- **Result**: hiển thị stats, thưởng.

### 13.2 Data
- Dùng **ScriptableObject** cho Weapon, Upgrade, Enemy, Drop.
- **Save**: JSON / PlayerPrefs cho meta.

### 13.3 Hệ thống chính
- **SpawnSystem**: spawn theo thời gian & curve difficulty.
- **EnemyAI**: di chuyển hướng nhân vật.
- **CombatSystem**: tính damage + knockback.
- **XPSystem**: gom exp → level up.
- **LootSystem**: drop chest, gold, heal.

### 13.4 Design patterns & cách sử dụng
- **Observer / Event**: phát sự kiện `OnEnemyKilled`, `OnLevelUp`, `OnPlayerDamaged`; UI, analytics, loot chỉ lắng nghe, tránh phụ thuộc chéo.
- **State Machine**: quản lý trạng thái game `Playing`, `Paused`, `LevelUp`, `GameOver`, `BossWarning`; mỗi state bật/tắt hệ thống phù hợp.
- **Strategy**: hành vi vũ khí/đòn đánh (projectile, AoE, orbit) tách thành strategy; đổi hành vi bằng data thay vì if/else dài.
- **Factory + Object Pool**: tạo enemy/bullet/drop theo loại từ pool; không `Instantiate/Destroy` liên tục khi gameplay chạy.
- **Command**: gói nâng cấp thành lệnh áp lên `Stats` (VD: +Damage, -Cooldown); dễ test và stack nhiều upgrade.
- **Data-Driven**: ScriptableObject định nghĩa chỉ số quái/vũ khí/nâng cấp; hệ thống đọc data để sinh entity.
- **Service Locator (nhẹ)**: truy cập hệ thống lõi (Spawn, XP, Loot) qua registry; tránh truyền tham chiếu khắp nơi nhưng giữ phạm vi nhỏ.
- **Composite**: gộp nhiều nâng cấp vào một build (nhiều modifier cộng dồn); xử lý hiệu ứng cộng gộp rõ ràng.

## 14. Difficulty scaling
- Tăng mật độ quái theo thời gian.
- Mở thêm loại quái sau mốc phút.
- Boss theo timer cố định.

## 15. KPI nội bộ
- Thời gian sống trung bình: 8–12 phút.
- Tỉ lệ quay lại run: >50% sau 3 run.
- Lần đầu chơi: hiểu loop trong 2 phút.

## 16. Rủi ro & phương án
- **Quá rối**: giảm số vũ khí xuất hiện trong 1 run.
- **Thiếu đa dạng**: thêm evolve, boss mechanic.
- **Hiệu năng**: tối ưu pooling, giới hạn số quái.

## 17. Milestones gợi ý
1. **Prototype**: di chuyển, spawn quái, auto attack.
2. **Vertical Slice**: 3 vũ khí, 2 loại quái, UI cơ bản.
3. **Alpha**: đủ systems, 1 boss.
4. **Beta**: balance, polish, meta progression.
5. **Release**: tối ưu, bugfix, content.

## 18. Test plan
- **Gameplay test**: cảm giác tăng sức mạnh có rõ không.
- **Performance test**: 200–500 quái cùng lúc.
- **Balance test**: vũ khí nào quá mạnh/ yếu.

