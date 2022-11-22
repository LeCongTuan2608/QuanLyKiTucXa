-- CREATE SCHEMA `kitucxa` ;

CREATE TABLE `kitucxa`.`loai_quyen` (
  `id_loai_quyen` INT NOT NULL PRIMARY KEY,
  `ten_quyen` NVARCHAR(50) NOT NULL
);
CREATE TABLE `kitucxa`.`toa` (
  `ma_toa` VARCHAR(20) NOT NULL PRIMARY KEY,
  `sl_phong` INT NOT NULL
);
CREATE TABLE `kitucxa`.`loai_phong` (
  `ma_loai` VARCHAR(20) NOT NULL PRIMARY KEY,
  `ten_loai` NVARCHAR(50) NOT NULL,
  `gia_tien` FLOAT NOT NULL
  );
CREATE TABLE `kitucxa`.`phong` (
  `ma_phong` VARCHAR(15) NOT NULL PRIMARY KEY,
  `suc_chua` INT NOT NULL,
  `sl_dang_o` INT NOT NULL,
  `ma_loai` VARCHAR(45) NOT NULL ,
  `ma_toa` VARCHAR(45) NOT NULL
  );
CREATE TABLE `kitucxa`.`user` (
  `id_user` VARCHAR(45) NOT NULL PRIMARY KEY,
  `cmnd` VARCHAR(45) NOT NULL,
  `ho_ten` NVARCHAR(100) NOT NULL,
  `gioi_tinh` NVARCHAR(40) NOT NULL,
  `ngay_sinh` DATE NOT NULL,
  `dia_chi` NVARCHAR(200) NOT NULL,
  `sdt` VARCHAR(45) NOT NULL,
  `ma_phong` VARCHAR(45),
  `image` NVARCHAR(255),
  
  `id_loai_quyen` INT NOT NULL DEFAULT 1
 );
CREATE TABLE `kitucxa`.`khen_ky_luat` (
  `id_kt_kl` int (11) AUTO_INCREMENT PRIMARY KEY,
  `hinh_thuc` NVARCHAR(50) NOT NULL,
  `mo_ta` NVARCHAR(200) NOT NULL,
  `ngay_tao` DATE,
  `id_user` VARCHAR(45) NOT NULL
  );
  CREATE TABLE `kitucxa`.`than_nhan` (
  `cmnd` VARCHAR(40) NOT NULL PRIMARY KEY,
  `ho_ten` NVARCHAR(50) NOT NULL,
  `gioi_tinh` NVARCHAR(45) NOT NULL,
  `quan_he_vs_sv` NVARCHAR(100) NOT NULL,
  `dia_chi` NVARCHAR(200) NOT NULL,
  `sdt` VARCHAR(45) NOT NULL,
  `id_user` VARCHAR(45) NOT NULL
);
CREATE TABLE `kitucxa`.`hop_dong` (
  `ma_hop_dong` int (11) AUTO_INCREMENT PRIMARY KEY,
  `ngay_bat_dau` DATE NOT NULL,
  `ngay_ket_thuc` DATE NOT NULL,
  `ngay_lap` DATE,
  `thanh_tien` FLOAT,
  `id_user` VARCHAR(45) NOT NULL
);
CREATE TABLE `kitucxa`.`hoa_don_dien_nuoc` (
  `ma_hoa_don` int (11) AUTO_INCREMENT PRIMARY KEY,
  `ngay_lap` DATE,
  `so_dien_dau` FLOAT NOT NULL,
  `so_dien_cuoi` FLOAT NOT NULL,
  `so_nuoc_dau` FLOAT NOT NULL,
  `so_nuoc_cuoi` FLOAT NOT NULL,
  
  `don_gia_dien` FLOAT NULL DEFAULT 1900,
  `don_gia_nuoc` FLOAT NULL DEFAULT 1900,
  `thanh_tien` FLOAT,
  `trang_thai` NVARCHAR(45) NULL DEFAULT 'Chưa thanh toán',
  `phuong_thuc_thanh_toan` NVARCHAR(200),
  
  `ma_phong` VARCHAR(45) NOT NULL
 );
 CREATE TABLE `kitucxa`.`tai_khoan` (
  `id_user` VARCHAR(45) NOT NULL,
  `mat_khau` VARCHAR(200) NOT NULL
  );
  
  -- --------------------------------------------
  ALTER TABLE `kitucxa`.`user` 
ADD INDEX `fk_id_loai_quyen_idx` (`id_loai_quyen` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`user` 
ADD CONSTRAINT `fk_id_loai_quyen`
  FOREIGN KEY (`id_loai_quyen`)
  REFERENCES `kitucxa`.`loai_quyen` (`id_loai_quyen`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `kitucxa`.`than_nhan` 
ADD INDEX `fk_id_user_idx` (`id_user` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`than_nhan` 
ADD CONSTRAINT `fk_id_user`
  FOREIGN KEY (`id_user`)
  REFERENCES `kitucxa`.`user` (`id_user`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


ALTER TABLE `kitucxa`.`tai_khoan` 
ADD INDEX `fk_tk_id_user_idx` (`id_user` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`tai_khoan` 
ADD CONSTRAINT `fk_tk_id_user`
  FOREIGN KEY (`id_user`)
  REFERENCES `kitucxa`.`user` (`id_user`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;



ALTER TABLE `kitucxa`.`phong` 
ADD INDEX `fk_ma_loai_idx` (`ma_loai` ASC) VISIBLE,
ADD INDEX `fk_ma_toa_idx` (`ma_toa` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`phong` 
ADD CONSTRAINT `fk_ma_loai`
  FOREIGN KEY (`ma_loai`)
  REFERENCES `kitucxa`.`loai_phong` (`ma_loai`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `fk_ma_toa`
  FOREIGN KEY (`ma_toa`)
  REFERENCES `kitucxa`.`toa` (`ma_toa`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `kitucxa`.`khen_ky_luat` 
ADD INDEX `fk_kt_kl_id_user_idx` (`id_user` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`khen_ky_luat` 
ADD CONSTRAINT `fk_kt_kl_id_user`
  FOREIGN KEY (`id_user`)
  REFERENCES `kitucxa`.`user` (`id_user`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


ALTER TABLE `kitucxa`.`hop_dong` 
ADD INDEX `fk_hop_dong_id_user_idx` (`id_user` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`hop_dong` 
ADD CONSTRAINT `fk_hop_dong_id_user`
  FOREIGN KEY (`id_user`)
  REFERENCES `kitucxa`.`user` (`id_user`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


ALTER TABLE `kitucxa`.`hoa_don_dien_nuoc` 
ADD INDEX `fk_ma_phong_idx` (`ma_phong` ASC) VISIBLE;
;
ALTER TABLE `kitucxa`.`hoa_don_dien_nuoc` 
ADD CONSTRAINT `fk_ma_phong`
  FOREIGN KEY (`ma_phong`)
  REFERENCES `kitucxa`.`phong` (`ma_phong`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;


-- -----------------------------ĐEFAULT VALUE-------------------------------
INSERT INTO `kitucxa`.`loai_quyen` (`id_loai_quyen`, `ten_quyen`) VALUES ('0', 'ADMIN');
INSERT INTO `kitucxa`.`loai_quyen` (`id_loai_quyen`, `ten_quyen`) VALUES ('1', 'USER');


INSERT INTO `kitucxa`.`loai_phong` (`ma_loai`, `ten_loai`, `gia_tien`) VALUES ('PT', 'Phòng thường', '210000');
INSERT INTO `kitucxa`.`loai_phong` (`ma_loai`, `ten_loai`, `gia_tien`) VALUES ('PV', 'Phòng vip', '350000');


INSERT INTO `kitucxa`.`toa` (`ma_toa`, `sl_phong`) VALUES ('A', '20');
INSERT INTO `kitucxa`.`toa` (`ma_toa`, `sl_phong`) VALUES ('B', '20');


INSERT INTO `kitucxa`.`phong` (`ma_phong`, `suc_chua`, `sl_dang_o`, `ma_loai`, `ma_toa`) VALUES ('A101', '4', '0', 'PT', 'A');
INSERT INTO `kitucxa`.`phong` (`ma_phong`, `suc_chua`, `sl_dang_o`, `ma_loai`, `ma_toa`) VALUES ('A102', '4', '0', 'PT', 'A');

INSERT INTO `kitucxa`.`user` (`id_user`, `cmnd`, `ho_ten`, `gioi_tinh`, `ngay_sinh`, `dia_chi`, `sdt`, `id_loai_quyen`) 
VALUES ('admin', '077202005527', 'admin', 'Nam', '2002/08/26', 'Vung Tau', '0377969735', '0');
	INSERT INTO tai_khoan (`id_user`, `mat_khau`) VALUES ('admin', '1');

-- -------------------------TRIGGER-----------------------------
DELIMITER $$
USE `kitucxa`$$
DROP TRIGGER IF EXISTS `kitucxa`.`add_user` $$
CREATE DEFINER=`root`@`localhost` TRIGGER `add_user` AFTER INSERT ON `user` FOR EACH ROW BEGIN

    UPDATE `phong` SET `sl_dang_o` = (SELECT COUNT(`id_user`) FROM `user`
	WHERE `user`.`ma_phong` = `phong`.`ma_phong`)
	WHERE `phong`.`ma_phong` = NEW.`ma_phong`;
	INSERT INTO tai_khoan (`id_user`, `mat_khau`) VALUES (NEW.id_user, '1');

END$$
DELIMITER ;

INSERT INTO `kitucxa`.`user` (`id_user`, `cmnd`, `ho_ten`, `gioi_tinh`, `ngay_sinh`, `dia_chi`, `sdt`, `ma_phong`) 
VALUES ('6151071030', '345345123123', 'Che Phan Hoang Viet', 'Nam', '2002/02/21', 'Binh Dinh', '234234', 'A101');




-- truy van so luong
select count(ma_phong) as tong_phong_trong, sum(suc_chua - phong.sl_dang_o) as tong_cho_trong 
from phong , `user` where phong.sl_dang_o < phong.suc_chua;
select count(id_user) as tong_SV  from `user` where id_user != 'admin';



-- truy van toa
select ma_toa from toa;



-- truy van phong
select ma_phong, suc_chua, sl_dang_o, ma_loai from phong where ma_toa ='A';

-- truy van thong tin nguoi dung
select `user`.cmnd, `user`.ho_ten, `user`.gioi_tinh, `user`.dia_chi, `user`.sdt,`user`.ma_phong, `user`.image,
year(`user`.ngay_sinh) as nam, month(`user`.ngay_sinh) as thang, day(`user`.ngay_sinh) as ngay
from `user`
where id_user = 'admin';



INSERT INTO `kitucxa`.`hoa_don_dien_nuoc` (`ngay_lap`, `so_dien_dau`, `so_dien_cuoi`, `so_nuoc_dau`, `so_nuoc_cuoi`, `thanh_tien`, `ma_phong`) VALUES ('2022-03-20', '0', '30', '0', '42', '234234', 'A101');

-- select tất cả thông tin hoa don 
select *, so_nuoc_cuoi - so_nuoc_dau as so_nuoc, so_dien_cuoi - so_dien_dau as so_dien 
from hoa_don_dien_nuoc, phong, `user` where  hoa_don_dien_nuoc.ma_phong = phong.ma_phong 
and `user`.ma_phong = phong.ma_phong and `user`.id_user = '6151071028';


-- select hóa đơn  
select hoa_don_dien_nuoc.ma_hoa_don, hoa_don_dien_nuoc.ma_phong, 
(hoa_don_dien_nuoc.so_nuoc_cuoi - hoa_don_dien_nuoc.so_nuoc_dau)*hoa_don_dien_nuoc.don_gia_nuoc as tien_nuoc,
(hoa_don_dien_nuoc.so_dien_cuoi - hoa_don_dien_nuoc.so_dien_dau)*hoa_don_dien_nuoc.don_gia_dien as tien_dien,
hoa_don_dien_nuoc.thanh_tien
from hoa_don_dien_nuoc,`user` where`user`.id_user = '6151071030'
and so_dien_cuoi = (SELECT MAX(so_dien_cuoi) FROM hoa_don_dien_nuoc where hoa_don_dien_nuoc.ma_phong = `user`.ma_phong and hoa_don_dien_nuoc.trang_thai = 'Chưa thanh toán') ;


select ho_ten, sdt from `user` where id_user = 'admin';


-- thanh toán
UPDATE hoa_don_dien_nuoc SET trang_thai = 'Đã thanh toán' , phuong_thuc_thanh_toan = 'MoMo'
WHERE ma_hoa_don = '2';







