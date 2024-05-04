package com.example.ebay_product_scrapper.repository;

import java.util.List;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import com.example.ebay_product_scrapper.entity.Product;

@Repository
public interface ProductRepository extends JpaRepository<Product, UUID> {
    @Query("SELECT p FROM Product p WHERE p.itemNumber = :itemNumber")
    Product findByItemNumber(@Param("itemNumber") String itemNumber);

    List<Product> findAllByItemNumberIsNotNullAndItemNumberIsNot(String itemNumber);

}
