package com.example.ebay_product_scrapper.repository;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.example.ebay_product_scrapper.entity.Image;

@Repository
public interface ImageRepository extends JpaRepository<Image, UUID> {

}
